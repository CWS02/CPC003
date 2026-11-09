using CPC02.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Globalization;

namespace CPC02.Services
{
    // Context lifetime is owned by the controller; all queries here are read-only.
    public sealed class EsgCalculationService
    {
        private readonly ESGContext _db;
        private readonly TWNCPCContext _TWNCPCdb;

        public EsgCalculationService(ESGContext db, TWNCPCContext accountingDb)
        {
            _db = db;
            _TWNCPCdb = accountingDb;
        }

        public IEnumerable<object> Calculate(SGS_Search search)
        {
            switch (search.category)
            {
                case "electricity": return CalculateElectricity(search);
                case "water": return CalculateWater(search);
                case "waste": return CalculateWaste(search);
                case "wastetransportation": return CalculateWasteTransportation(search);
                case "fireextin": return CalculateFireExtinguishers(search);
                case "wd40": return CalculateWd40(search);
                case "coldcoal": return CalculateRefrigerants(search);
                case "commuting": return CalculateCommuting(search);
                case "traffic": return CalculateTraffic(search);
                case "公務車": return CalculateOfficialCars(search);
                case "elec_UP": return CalculateElectricityUpstream(search);
                case "gas_UP": return CalculateGasolineUpstream(search);
                case "diesel": return CalculateDiesel(search);
                case "diesel_UP": return CalculateDieselUpstream(search);
                default: return new List<object>();
            }
        }

        // electricity
        private IEnumerable<object> CalculateElectricity(SGS_Search search)
        {
            var yearStart = new DateTime(int.Parse(search.year), 1, 1);
            var yearEnd = yearStart.AddYears(1);
            var query = _db.ELECTRICITY_BILL.AsNoTracking()
                .Where(x => (search.factory == "全部" || x.FACTORY == search.factory));
            if (!string.IsNullOrEmpty(search.year))
            {
                query = query.Where(x => x.FROM_BILLING_PERIOD >= yearStart && x.FROM_BILLING_PERIOD < yearEnd);
            }
            var result = query
                .GroupBy(x => x.FACTORY)
                .Select(g => new ElectricitySummaryViewModel
            {
                Factory = g.Key,
                SumPeakElectricity = g.Sum(x => x.PEAK_ELECTRICITY),
                SumHalfSpikePower = g.Sum(x => x.HALF_SPIKE_POWER),
                SumSaturdayHalfPeak = g.Sum(x => x.SATURDAY_HALF_PEAK),
                SumOffPeakElectricity = g.Sum(x => x.OFF_PEAK_ELECTRICITY),
                SumTotalElectricity = g.Sum(x => x.TOTAL_ELECTRICITY),
                SumTotalBillTax = g.Sum(x => x.TOTAL_BILL_TAX),
                SumCarbonPeriod = g.Sum(x => x.CARBON_PERIOD)
            }).ToList();

            return result;
        }

        // water
        private IEnumerable<object> CalculateWater(SGS_Search search)
        {
            var yearStart = new DateTime(int.Parse(search.year), 1, 1);
            var yearEnd = yearStart.AddYears(1);
            var query = _db.WATER_BILL.AsNoTracking()
                .Where(x => (search.factory == "全部" || x.FACTORY == search.factory));
            if (!string.IsNullOrEmpty(search.year))
            {
                query = query.Where(x => x.FROM_BILLING_PERIOD >= yearStart && x.FROM_BILLING_PERIOD < yearEnd);
            }
            var result = query
                .GroupBy(x => new { x.FACTORY, x.METER_DIAMETER })
                .OrderBy(g => g.Key.METER_DIAMETER)
                .Select(g => new WaterSummaryViewModel
            {
                Factory = g.Key.FACTORY,
                Waterdiameter = g.Key.METER_DIAMETER.ToString(),
                SumNUMBERPOINTERS = g.Sum(x => x.NUMBER_POINTERS),
                SumTOTALWATER = g.Sum(x => x.TOTAL_WATER),
                SumTOTALBILLTAX = g.Sum(x => x.TOTAL_BILL_TAX),
                SumCARBONPERIOD = g.Sum(x => x.CARBON_PERIOD),
            }).ToList();

            return result;
        }

        // waste
        private IEnumerable<object> CalculateWaste(SGS_Search search)
        {
            // Evaluate AsNoTracking before building the expression tree. EF6 cannot
            // rewrite DbQuery.AsNoTracking calls embedded in a correlated projection.
            var wastes = _db.WASTES.AsNoTracking();
            var settings = _db.SGS_ParameterSetting.AsNoTracking();
            var parameters = _db.SGS_Parameter.AsNoTracking();
            return BuildWasteSummaryQuery(wastes, settings, parameters, search).ToList();
        }

        private static IQueryable<WasteSummaryViewModel> BuildWasteSummaryQuery(
            IQueryable<WASTES> wastes,
            IQueryable<SGS_ParameterSetting> settings,
            IQueryable<SGS_Parameter> parameters,
            SGS_Search search)
        {
            var yearStart = new DateTime(int.Parse(search.year), 1, 1);
            var yearEnd = yearStart.AddYears(1);
            var query = wastes;
            if (search.methods != "全部")
            {
                query = wastes.Where(x => (search.methods == "全部" || x.TREATMENT == search.methods));
            }

            if (!string.IsNullOrEmpty(search.year))
            {
                query = query.Where(x => x.REMOVAL_DATE >= yearStart && x.REMOVAL_DATE < yearEnd);
            }
            var result = query
                .GroupBy(x => new { x.TREATMENT, x.SCRAP_CODE })
                .Select(g => new WasteSummaryViewModel
            {
                methods = g.Key.TREATMENT,
                code = g.Key.SCRAP_CODE,
                SumDECLAREDWEIGHT = g.Sum(x => x.DECLARED_WEIGHT),
                SumCKILOMETERS = g.Sum(x => x.KILOMETERS),
                SumACTIVITYDATA = g.Sum(x => x.ACTIVITY_DATA),
                SumCARBONEMISSIONFACTOR = g.Sum(x => x.CARBON_EMISSION_FACTOR),
                SumCCARBONDIOXIDE = g.Sum(x => x.CARBON_DIOXIDE),
                coefficient = (
                from ss in settings
                join s in parameters on ss.PAR001.ToString() equals s.PAR000
                where ss.PAR003 == "waste" && ss.PAR004 == g.Key.TREATMENT
                && ss.Year.ToString() == search.year && s.PAR007 == 0
                orderby ss.PAR000
                select s.PAR002
                ).FirstOrDefault()
            });

            return result;
        }

        // wastetransportation
        private IEnumerable<object> CalculateWasteTransportation(SGS_Search search)
        {
            var yearStart = new DateTime(int.Parse(search.year), 1, 1);
            var yearEnd = yearStart.AddYears(1);
            var query = _db.WASTES.AsNoTracking()
                .Where(x => (search.methods == "全部" || x.TREATMENT == search.methods));
            if (!string.IsNullOrEmpty(search.year))
            {
                query = query.Where(x => x.REMOVAL_DATE >= yearStart && x.REMOVAL_DATE < yearEnd);
            }
            var result = query
                .GroupBy(x => new { x.TREATMENT, x.SCRAP_CODE })
                .Select(g => new WastetransportationViewModel
            {
                methods = g.Key.TREATMENT,
                code = g.Key.SCRAP_CODE,
                SumDECLAREDWEIGHT = g.Sum(x => x.DECLARED_WEIGHT),
                SumCKILOMETERS = g.Sum(x => x.KILOMETERS),
                SumACTIVITYDATA = g.Sum(x => x.ACTIVITY_DATA),
                SumCARBONEMISSIONFACTOR = g.Sum(x => x.CARBON_EMISSION_FACTOR),
                SumCCARBONDIOXIDE = g.Sum(x => x.CARBON_DIOXIDE),
            }).ToList();

            return result;
        }

        // fireextin
        private IEnumerable<object> CalculateFireExtinguishers(SGS_Search search)
        {
            var query = _db.FireExtin.AsNoTracking()
                .Where(x => (search.factory == "全部" || x.FE010 == search.factory) && x.Year.ToString()==search.year)
                .GroupBy(x => new { x.FE010, x.FE005 })
                .Select(g => new FireExtinViewModel
            {
                Factory = g.Key.FE010,
                content = g.Key.FE005,
                SumFE002 = g.Sum(x => x.FE002),
                Sumton = g.Sum(x =>
                (x.FE001 != null && !x.FE001.Contains("乾粉") && x.FE002.HasValue && x.FE006.HasValue)
                ? (x.FE002.Value * x.FE006.Value / 1000)
                : 0),
                Sumkg = g.Sum(x =>
                (x.FE001 != null && !x.FE001.Contains("乾粉") && x.FE002.HasValue && x.FE006.HasValue)
                ? (x.FE002.Value * x.FE006.Value)
                : 0)
            })
            .ToList();


            return query;
        }

        // wd40
        private IEnumerable<object> CalculateWd40(SGS_Search search)
        {
            return _db.WD40A.AsNoTracking()
                .Where(x => x.WD003.Contains(search.year) && x.Status == 0)
                .GroupBy(x => 1)
                .Select(g => new WD40ViewModel { SumWD011 = g.Sum(x => x.WD011) }).ToList();
        }

        // coldcoal
        private IEnumerable<object> CalculateRefrigerants(SGS_Search search)
        {
            var refrigerants = new[] { "R-134A", "HFC-134A", "R-407C", "R-410A" };
            var result = _db.ColdCoal.AsNoTracking()
                .Where(x => (search.factory == "全部" || x.CC010 == search.factory) && refrigerants.Contains(x.CC007))
                .GroupBy(x => new { x.CC010, x.CC007 })
                .Select(g => new ColdCoalViewModel
            {
                Factory = g.Key.CC010,
                Code = g.Key.CC007,
                SumCC012 = Math.Round(g.Sum(x => x.CC012) ?? 0m, 4)
            }).ToList();
            // Preserve the GWP values used in the existing report.
            var gwp = new Dictionary<string, int>
            {
                { "R-134A", 1526 }, { "HFC-134A", 1526 }, { "R-407C", 1908 }, { "R-410A", 2256 }
            };
            foreach (var item in result) item.GWP = gwp[item.Code.Trim()];
            return result;
        }

        // commuting
        private IEnumerable<object> CalculateCommuting(SGS_Search search)
        {
            var factors = BuildCommutingFactorQuery(_db.SGS_ParameterSetting.AsNoTracking(),
                _db.SGS_Parameter.AsNoTracking(), int.Parse(search.year)).ToList();
            var activity = (from c in _db.CAT_THREE_EMPLOYEE_COMMUTING.AsNoTracking()
                            join g in _db.GHG_MST_COMMUTE.AsNoTracking() on c.USER_ID equals g.USER_ID
                            where c.WORK_DATE.StartsWith(search.year) && g.TRANSPORTATION != "公務車"
                            group g by g.TRANSPORTATION into rows
                            select new CommutingViewModel
                            {
                                TRANSPORTATION = rows.Key,
                                TotalDoubleKM = rows.Sum(x => x.DOUBLE_KM)
                            }).ToList();
            foreach (var row in activity)
            {
                row.EF_VALUE = ResolveCommutingFactor(
                    factors.Where(x => x.Method == row.TRANSPORTATION).Select(x => x.Value), row.TRANSPORTATION);
            }
            return activity;
        }

        private static IQueryable<CommutingFactorSetting> BuildCommutingFactorQuery(
            IQueryable<SGS_ParameterSetting> settings, IQueryable<SGS_Parameter> parameters, int year)
        {
            return from setting in settings
                   join parameter in parameters on setting.PAR001.ToString() equals parameter.PAR000
                   where setting.PAR002 == "通勤" && setting.Year == year && parameter.PAR007 == 0
                   select new CommutingFactorSetting { Method = setting.PAR004, Value = parameter.PAR002 };
        }

        private static decimal ResolveCommutingFactor(IEnumerable<string> values, string transportation)
        {
            var factors = new List<decimal>();
            foreach (var value in values)
            {
                decimal factor;
                if (!decimal.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out factor))
                    throw new EsgConfigurationException("通勤方式「" + transportation + "」的排放係數格式不正確。");
                factors.Add(factor);
            }
            if (factors.Count == 0)
                throw new EsgConfigurationException("此年度的通勤方式「" + transportation + "」缺少有效的通勤係數設定。");
            if (factors.Distinct().Count() > 1)
                throw new EsgConfigurationException("此年度的通勤方式「" + transportation + "」設定了多個不同係數，請先確認通勤係數設定。");
            return factors[0];
        }

        private sealed class CommutingFactorSetting
        {
            public string Method { get; set; }
            public string Value { get; set; }
        }

        // traffic
        private IEnumerable<object> CalculateTraffic(SGS_Search search)
        {
            var methods = new[] { "高鐵", "飛機", "計程車", "捷運", "台灣火車", "地鐵", "私車公用" };
            var totals = GetTrafficAccountingActivity(search.methods, search.year)
                .Where(x => methods.Any(m => x.Type.StartsWith(m, StringComparison.Ordinal)))
                .GroupBy(x => x.Type.StartsWith("捷運") || x.Type.StartsWith("地鐵")
                    ? x.Type : methods.First(m => x.Type.StartsWith(m, StringComparison.Ordinal)))
                .Select(g => new AccountingActivity { Type = g.Key, Amount = g.Sum(x => x.Amount) }).ToList();
            var settings = _db.SGS_ParameterSetting.AsNoTracking();
            var coefficients = _db.SGS_Parameter.AsNoTracking();
            var parameters = (from setting in settings
                              join parameter in coefficients on setting.PAR001.ToString() equals parameter.PAR000
                              where setting.PAR003 == "traffic" && setting.Year.ToString() == search.year && parameter.PAR007 == 0
                              orderby setting.PAR000
                              select new { Type = setting.PAR005, Method = setting.PAR004, Value = parameter.PAR002 }).ToList();
            return totals.OrderBy(x => x.Type).Select(x => new TrafficViewModel
            {
                Type = x.Type,
                TotalEmission = x.Amount,
                ParameterValue = parameters.Where(p => p.Type == x.Type).Select(p => p.Value).FirstOrDefault()
                    ?? parameters.Where(p => p.Method == x.Type).Select(p => p.Value).FirstOrDefault()
            }).ToList();
        }

        // 公務車
        private IEnumerable<object> CalculateOfficialCars(SGS_Search search)
        {
            var coefficients = GetGasCoefficients(search.category, int.Parse(search.year));
            var total = GetAccountingActivity(search.category, search.year).Sum(x => x.Amount);
            return new List<OfficialcarViewModel>
            {
                new OfficialcarViewModel
                {
                    Type = search.category,
                    TotalEmission = total,
                    GasCoefficients = coefficients,
                    TotalCO2eEmission = CalculateGasEmission(total, coefficients)
                }
            };
        }

        // elec_UP
        private IEnumerable<object> CalculateElectricityUpstream(SGS_Search search)
        {
            var yearStart = new DateTime(int.Parse(search.year), 1, 1);
            var yearEnd = yearStart.AddYears(1);
            var query = _db.ELECTRICITY_BILL.AsNoTracking().Where(x => x.FACTORY == "南科廠")
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.year))
            {
                query = query.Where(x => x.FROM_BILLING_PERIOD >= yearStart && x.FROM_BILLING_PERIOD < yearEnd);
            }

            // 總加總
            var totalElectricity = query.Sum(x => x.TOTAL_ELECTRICITY);

            var result = new List<Elec_UPViewModel>
            {
                new Elec_UPViewModel
                {
                    SumTotalElectricity = totalElectricity
                }
            };

            return result;
        }

        // gas_UP
        private IEnumerable<object> CalculateGasolineUpstream(SGS_Search search)
        {
            return new List<Gas_UPViewModel>
            {
                new Gas_UPViewModel
                {
                    Type = "合計",
                    TotalEmission = GetAccountingActivity("公務", search.year).Sum(x => x.Amount)
                }
            };
        }

        // diesel
        private IEnumerable<object> CalculateDiesel(SGS_Search search)
        {
            var year = int.Parse(search.year);
            var coefficients = GetGasCoefficients(search.category, year);
            var result = _db.Diesel.AsNoTracking().Where(x => x.DI001 == year)
                .GroupBy(x => x.DI001)
                .Select(g => new Diesel_ViewModel
            {
                Type = "緊急發電機（柴油）",
                Total = g.Sum(x => x.DI008)
            }).ToList();
            foreach (var item in result)
            {
                item.GasCoefficients = coefficients;
                item.TotalCO2eEmission = CalculateGasEmission(item.Total, coefficients);
            }
            return result;
        }

        // diesel_UP
        private IEnumerable<object> CalculateDieselUpstream(SGS_Search search)
        {
            var query = _db.Diesel.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(search.year))
            {
                query = query.Where(x => x.DI001.ToString() == search.year);
            }

            var result = query.GroupBy(x => x.DI001).Select(g => new Diesel_UPViewModel
            {
                Type = "",
                Total = g.Sum(x => x.DI008)
            }).ToList();

            return result;
        }

        private List<GasCoefficient> GetGasCoefficients(string category, int year)
        {
            var coefficients = _db.Gas_Parameter.AsNoTracking()
                .Where(x => x.GP001 == category && x.GP002 == year).OrderBy(x => x.GP003)
                .Select(x => new GasCoefficient
            {
                GasName = x.GP003,
                Coefficient1 = x.GP004 ?? 0m,
                Coefficient2 = x.GP005 ?? 0m
            }).ToList();
            if (coefficients.Count == 0)
                throw new EsgConfigurationException("此年度尚未設定 " + category + " 的溫室氣體係數。");
            return coefficients;
        }

        private static decimal CalculateGasEmission(decimal liters, IEnumerable<GasCoefficient> coefficients)
        {
            return coefficients.Sum(x => liters * x.Coefficient1 * x.Coefficient2 / 1000m);
        }

        private List<AccountingActivity> GetTrafficAccountingActivity(string method, string year)
        {
            var expenses = BuildTrafficExpenseQuery(_TWNCPCdb.ACPTA.AsNoTracking(),
                _TWNCPCdb.ACPTB.AsNoTracking(), method, year).ToList();
            var payments = BuildTrafficPaymentQuery(_TWNCPCdb.PCMTG.AsNoTracking(), method, year).ToList();
            // Only the grouped traffic rows reach memory; keep types present in either source.
            return expenses.Concat(payments).GroupBy(x => x.Type)
                .Select(g => new AccountingActivity { Type = g.Key, Amount = g.Sum(x => x.Amount) }).ToList();
        }

        private static IQueryable<AccountingActivity> BuildTrafficExpenseQuery(
            IQueryable<ACPTA> headers, IQueryable<ACPTB> details, string method, string year)
        {
            details = details.Where(x => x.TB008.StartsWith(year));
            if (method == "全部")
            {
                details = details.Where(x => x.UDF01.StartsWith("高鐵") || x.UDF01.StartsWith("飛機")
                    || x.UDF01.StartsWith("計程車") || x.UDF01.StartsWith("捷運")
                    || x.UDF01.StartsWith("台灣火車") || x.UDF01.StartsWith("地鐵")
                    || x.UDF01.StartsWith("私車公用"));
            }
            else
            {
                details = details.Where(x => x.UDF01.StartsWith(method));
            }
            // ERP document keys are fixed-width keys. SQL Server equality handles
            // trailing padding, so avoid wrapping indexed join columns in Trim().
            return from detail in details
                   join header in headers
                       on new { Type = detail.TB001, Number = detail.TB002 }
                       equals new { Type = header.TA001, Number = header.TA002 }
                   group detail by detail.UDF01 into rows
                   select new AccountingActivity { Type = rows.Key, Amount = rows.Sum(x => x.UDF06) ?? 0m };
        }

        private static IQueryable<AccountingActivity> BuildTrafficPaymentQuery(
            IQueryable<PCMTG> payments, string method, string year)
        {
            payments = payments.Where(x => x.TG013.StartsWith(year));
            if (method == "全部")
            {
                payments = payments.Where(x => x.UDF01.StartsWith("高鐵") || x.UDF01.StartsWith("飛機")
                    || x.UDF01.StartsWith("計程車") || x.UDF01.StartsWith("捷運")
                    || x.UDF01.StartsWith("台灣火車") || x.UDF01.StartsWith("地鐵")
                    || x.UDF01.StartsWith("私車公用"));
            }
            else
            {
                payments = payments.Where(x => x.UDF01.StartsWith(method));
            }
            return payments.GroupBy(x => x.UDF01)
                .Select(g => new AccountingActivity { Type = g.Key, Amount = g.Sum(x => x.UDF06) ?? 0m });
        }

        private List<AccountingActivity> GetAccountingActivity(string prefix, string year)
        {
            var expenses = (from a in _TWNCPCdb.ACPTA.AsNoTracking()
            join b in _TWNCPCdb.ACPTB.AsNoTracking()
            on new { Type = a.TA001.Trim(), Number = a.TA002.Trim() }
            equals new { Type = b.TB001.Trim(), Number = b.TB002.Trim() }
            where b.UDF01.StartsWith(prefix) && b.TB008.StartsWith(year)
            group b by b.UDF01 into rows
            select new AccountingActivity
            {
                Type = rows.Key, Amount = rows.Sum(x => x.UDF06) ?? 0m
            }).ToList();
            var payments = _TWNCPCdb.PCMTG.AsNoTracking()
                .Where(x => x.UDF01.StartsWith(prefix) && x.TG013.StartsWith(year))
                .GroupBy(x => x.UDF01)
                .Select(g => new AccountingActivity { Type = g.Key, Amount = g.Sum(x => x.UDF06) ?? 0m }).ToList();
            // Either source can contain a type absent from the other source.
            return expenses.Concat(payments).GroupBy(x => x.Type)
                .Select(g => new AccountingActivity { Type = g.Key, Amount = g.Sum(x => x.Amount) })
            .OrderBy(x => x.Type).ToList();
        }

        private sealed class AccountingActivity
        {
            public string Type { get; set; }
            public decimal Amount { get; set; }
        }
    }
}
