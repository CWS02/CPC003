using CPC02.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPC02.Controllers
{
    public class ESGController : Controller
    {
        ESGContext _db = new ESGContext();
        TWNCPCContext _TWNCPCdb = new TWNCPCContext();
        [HttpGet]
        public ActionResult SGS_Calculate(SGS_Search search)
        {

            var coefficientOptions = (from p in _db.SGS_Parameter
                                      join s in _db.SGS_ParameterSetting
                                      on p.PAR000 equals s.PAR001.ToString() into joined
                                      from s in joined.DefaultIfEmpty()
                                      select new
                                      {
                                          Id = p.PAR000,
                                          Label = p.PAR002 + "-" + p.PAR001 + "-" + p.PAR004,
                                          Value = p.PAR002
                                      }).ToList();

            var allParameters = _db.SGS_ParameterSetting
            .Select(s => new
            {
                Id = s.PAR001,
                Category = s.PAR003,
                Category2 = s.PAR004,
            }).ToList();

            ViewBag.AllParameters = allParameters;
            ViewBag.CoefficientOptions = coefficientOptions;


            if (search.category == "electricity")
            {
                var query = _db.ELECTRICITY_BILL
               .Where(x => x.FACTORY == search.factory);
                if (!string.IsNullOrEmpty(search.year))
                {
                    query = query.Where(x => x.FROM_BILLING_PERIOD.Value.Year.ToString() == search.year);
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

                return View(result);
            }
            else if (search.category == "water")
            {
                var query = _db.WATER_BILL
               .Where(x => x.FACTORY == search.factory && x.METER_DIAMETER.ToString() == search.waterdiameter);
                if (!string.IsNullOrEmpty(search.year))
                {
                    query = query.Where(x => x.FROM_BILLING_PERIOD.Value.Year.ToString() == search.year);
                }
                var result = query
                .GroupBy(x => x.FACTORY)
                .Select(g => new WaterSummaryViewModel
                {
                    Factory = g.Key,
                    Waterdiameter = search.waterdiameter,
                    SumNUMBERPOINTERS = g.Sum(x => x.NUMBER_POINTERS),
                    SumTOTALWATER = g.Sum(x => x.TOTAL_WATER),
                    SumTOTALBILLTAX = g.Sum(x => x.TOTAL_BILL_TAX),
                    SumCARBONPERIOD = g.Sum(x => x.CARBON_PERIOD),
                }).ToList();

                return View(result);
            }
            else if (search.category == "waste")
            {
                var query = _db.WASTES
               .Where(x => x.TREATMENT == search.methods && x.SCRAP_CODE == search.code);
                if (!string.IsNullOrEmpty(search.year))
                {
                    query = query.Where(x => x.REMOVAL_DATE.Value.Year.ToString() == search.year);
                }
                var result = query
                               .GroupBy(x => x.TREATMENT)
                               .Select(g => new WasteSummaryViewModel
                               {
                                   methods = search.methods,
                                   code = search.code,
                                   SumDECLAREDWEIGHT = g.Sum(x => x.DECLARED_WEIGHT),
                                   SumCKILOMETERS = g.Sum(x => x.KILOMETERS),
                                   SumACTIVITYDATA = g.Sum(x => x.ACTIVITY_DATA),
                                   SumCARBONEMISSIONFACTOR = g.Sum(x => x.CARBON_EMISSION_FACTOR),
                                   SumCCARBONDIOXIDE = g.Sum(x => x.CARBON_DIOXIDE),
                               }).ToList();

                return View(result);
            }
            else if (search.category == "fireextin")
            {
                var query = _db.FireExtin
                    .Where(x => x.FE010 == search.factory && x.FE005 == search.content)
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


                return View(query);
            }
            else if (search.category == "wd40")
            {
                if (search.year != null)
                {
                    var query = _db.WD40A
                   .Where(x => x.WD003.Contains(search.year))
                   .GroupBy(x => x.WD003.Contains(search.year))
                   .Select(g => new WD40ViewModel
                   {
                       SumWD011 = g.Sum(x => x.WD011),
                   })
                   .ToList();

                    return View(query);
                }
            }
            else if (search.category == "coldcoal")
            {
                if (search.factory != null)
                {
                    var validCC007Values = new List<string> { "R-134A", "HFC-134A", "R-407C", "R-410A" };

                    var query = _db.ColdCoal
                        .Where(x => x.CC010 == search.factory && validCC007Values.Contains(x.CC007))
                        .GroupBy(x => new { x.CC007, x.CC010 })
                        .Select(g => new ColdCoalViewModel
                        {
                            Code = g.Key.CC007,
                            SumCC012 = g.Sum(x => x.CC012),
                        })
                        .ToList();

                    return View(query);
                }
            }
            else if (search.category == "commuting")
            {
                var emissionFactors = _db.BRM_MST_EMISSION_FACTOR
                    .Where(b => b.EF_YEAR == search.year)
                .GroupBy(b => b.EF_NAME)
                .Select(g => g.FirstOrDefault());

                var result = (from c in _db.CAT_THREE_EMPLOYEE_COMMUTING
                              join g in _db.GHG_MST_COMMUTE on c.USER_ID equals g.USER_ID
                              join b in emissionFactors on g.TRANSPORTATION equals b.EF_NAME
                              where c.WORK_DATE.Substring(0, 4) == search.year
                              group new { g, b } by g.TRANSPORTATION into grp
                              select new CommutingViewModel
                              {
                                  TRANSPORTATION = grp.Key,
                                  TotalDoubleKM = grp.Sum(x => x.g.DOUBLE_KM),
                                  EF_VALUE = grp.Select(x => x.b.EF_VALUE).FirstOrDefault()
                              }).ToList();

                return View(result);
            }
            else if (search.category == "traffic")
            {
                var trafficList = new List<TrafficViewModel>();
                var sumACPTB = (
                    from a in _TWNCPCdb.ACPTA
                    join b in _TWNCPCdb.ACPTB
                        on new { Key1 = a.TA001.Trim(), Key2 = a.TA002.Trim() }
                        equals new { Key1 = b.TB001.Trim(), Key2 = b.TB002.Trim() }
                    where (b.UDF01 ?? "").StartsWith(search.methods)
                       && (a.TA015 ?? "").Substring(0, 4) == search.year
                    select (decimal?)b.UDF06
                ).Sum() ?? 0;

                var sumPCMTG = _TWNCPCdb.PCMTG
                    .Where(x => x.UDF01.StartsWith(search.methods) && x.TG013.Substring(0, 4) == search.year)
                    .Sum(x => (decimal?)x.UDF06) ?? 0;

                trafficList.Add(new TrafficViewModel
                {
                    TotalEmission = sumACPTB + sumPCMTG
                });

                return View(trafficList);
            }
            return View();
        }
    }
}