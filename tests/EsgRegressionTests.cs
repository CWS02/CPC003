using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Web.Mvc;
using CPC02.Controllers;
using CPC02.Models;
using CPC02.Services;

internal static class EsgRegressionTests
{
    private static void Check(bool condition, string description)
    {
        if (!condition) throw new Exception(description);
    }

    public static void Main()
    {
        using (var controller = new ESGController())
        {
            var result = (ViewResult)controller.SGS_Calculate(null);
            var model = (EsgCalculationPageViewModel)result.Model;
            Check(controller.ModelState.IsValid, "Initial page must render without querying reports.");
            Check(model.Search.category == "coldcoal" && model.Search.factory == "全部", "Initial category must be refrigerants with all factories.");
            Check(model.Search.year == Math.Max(2024, DateTime.Today.Year - 1).ToString(), "Default to last year, minimum 2024.");
        }
        foreach (var year in new[] { "bad", "0", "2023", "9999", "-1" })
        {
            using (var controller = new ESGController())
            {
                var result = controller.SGS_Calculate(new SGS_Search { category = "electricity", year = year });
                Check(result is ViewResult && !controller.ModelState.IsValid, "Invalid year must not query database.");
            }
        }
        using (var controller = new ESGController())
        {
            controller.SGS_Calculate(new SGS_Search { category = "unknown", year = "2026" });
            Check(!controller.ModelState.IsValid, "Unknown category must be rejected.");
        }
        var applyDefaults = typeof(ESGController).GetMethod("ApplySearchDefaults", BindingFlags.NonPublic | BindingFlags.Static);
        foreach (var category in new[] { "electricity", "water", "fireextin", "coldcoal", "traffic", "waste", "wastetransportation" })
        {
            var search = new SGS_Search { category = category, content = "CO2", waterdiameter = "40" };
            applyDefaults.Invoke(null, new object[] { search });
            Check(!string.IsNullOrWhiteSpace(search.factory) && !string.IsNullOrWhiteSpace(search.methods), "Search conditions must have defaults.");
            Check(search.factory == "全部" && search.methods == "全部", "All filters default to all.");
            Check(search.content == null && search.waterdiameter == null, "Removed filters must be ignored in bookmarked URLs.");
        }
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");
        var waste = new WasteSummaryViewModel { coefficient = "0.5", SumDECLAREDWEIGHT = 100m };
        Check(waste.EmissionTon == 0.05m, "Stored decimal factors must not depend on request culture.");
        waste.coefficient = "invalid";
        Check(waste.EmissionTon == null, "Missing or invalid factor must not become zero emissions.");
        waste.coefficient = "0";
        Check(waste.EmissionTon == 0m, "Zero is a valid factor.");
        var calculate = typeof(EsgCalculationService).GetMethod("CalculateGasEmission", BindingFlags.NonPublic | BindingFlags.Static);
        var factors = new List<GasCoefficient>
        {
            new GasCoefficient { Coefficient1 = 2m, Coefficient2 = 1m },
            new GasCoefficient { Coefficient1 = 0.1m, Coefficient2 = 25m }
        };
        Check((decimal)calculate.Invoke(null, new object[] { 100m, factors }) == 0.45m, "Gas totals include all gases and convert kg to tonnes once.");
        WasteQueryTranslationTests.Run();
        TrafficQueryTests.Run();
        CommutingFactorTests.Run();
        Console.WriteLine("ESG backend regression checks passed.");
    }
}
