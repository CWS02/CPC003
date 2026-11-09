using CPC02.Models;
using CPC02.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace CPC02.Controllers
{
    public class ESGController : Controller
    {
        private readonly ESGContext _db = new ESGContext();
        private readonly TWNCPCContext _accountingDb = new TWNCPCContext();

        [HttpGet]
        public ActionResult SGS_Calculate(SGS_Search search)
        {
            search = search ?? new SGS_Search();
            var hasSearched = !string.IsNullOrWhiteSpace(search.category);
            ApplySearchDefaults(search);
            var model = new EsgCalculationPageViewModel { Search = search, HasSearched = hasSearched };
            int year;
            if (!int.TryParse(search.year, out year) || year < 2024 || year > 9998)
                ModelState.AddModelError("year", "請輸入有效的西元年份（2024～9998）。");
            else
                search.year = year.ToString(CultureInfo.InvariantCulture);

            var categories = new[] { "electricity", "water", "waste", "wastetransportation", "fireextin",
                "wd40", "coldcoal", "commuting", "traffic", "公務車", "elec_UP", "gas_UP", "diesel", "diesel_UP" };
            if (search.category.Length > 0 && !categories.Contains(search.category))
                ModelState.AddModelError("category", "不支援此計算類別。");
            if (new[] { "electricity", "water", "fireextin", "coldcoal" }.Contains(search.category)
                && string.IsNullOrWhiteSpace(search.factory))
                ModelState.AddModelError("factory", "請選擇廠區。");
            if (new[] { "traffic", "wastetransportation", "waste" }.Contains(search.category)
                && string.IsNullOrWhiteSpace(search.methods))
                ModelState.AddModelError("methods", "請選擇處理方式或交通類別。");
            if (!ModelState.IsValid || !hasSearched)
                return View(model);

            model.CoefficientOptions = _db.SGS_Parameter.AsNoTracking()
                .Where(p => p.PAR007 == 0).OrderBy(p => p.PAR001).ThenBy(p => p.PAR000)
                .Select(p => new EsgCoefficientOption
                {
                    Id = p.PAR000, Label = p.PAR002 + "-" + p.PAR001 + "-" + p.PAR004, Value = p.PAR002
                }).ToList();
            model.Parameters = _db.SGS_ParameterSetting.AsNoTracking()
                .Where(p => p.Year == year && p.PAR003 == search.category).OrderBy(p => p.PAR000)
                .Select(p => new EsgCoefficientSetting
                {
                    Id = p.PAR001.ToString(), Category = p.PAR003, Category2 = p.PAR004, Year = p.Year
                }).ToList();
            try
            {
                model.Results = new EsgCalculationService(_db, _accountingDb).Calculate(search);
            }
            catch (EsgConfigurationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }

        private static void ApplySearchDefaults(SGS_Search search)
        {
            search.category = string.IsNullOrWhiteSpace(search.category) ? "coldcoal" : search.category.Trim();
            search.year = string.IsNullOrWhiteSpace(search.year)
                ? Math.Max(2024, DateTime.Today.Year - 1).ToString(CultureInfo.InvariantCulture) : search.year.Trim();
            if (string.IsNullOrWhiteSpace(search.factory))
                search.factory = "全部";
            if (string.IsNullOrWhiteSpace(search.methods))
                search.methods = "全部";
            // These fields are no longer search filters, including in older bookmarked URLs.
            search.content = null;
            search.waterdiameter = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
                _accountingDb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
