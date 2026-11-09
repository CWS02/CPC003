using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using CPC02.Models;
using CPC02.Services;

internal static class CommutingFactorTests
{
    public static void Run()
    {
        var id = Guid.NewGuid();
        var disabledId = Guid.NewGuid();
        var settings = new[]
        {
            new SGS_ParameterSetting { PAR001 = id, PAR002 = "通勤", PAR004 = "汽車", Year = 2025 },
            new SGS_ParameterSetting { PAR001 = id, PAR002 = "交通", PAR004 = "汽車", Year = 2025 },
            new SGS_ParameterSetting { PAR001 = id, PAR002 = "通勤", PAR004 = "汽車", Year = 2024 },
            new SGS_ParameterSetting { PAR001 = disabledId, PAR002 = "通勤", PAR004 = "機車", Year = 2025 }
        }.AsQueryable();
        var parameters = new[]
        {
            new SGS_Parameter { PAR000 = id.ToString(), PAR002 = "0.115", PAR007 = 0 },
            new SGS_Parameter { PAR000 = disabledId.ToString(), PAR002 = "0.0951", PAR007 = 1 }
        }.AsQueryable();
        var build = typeof(EsgCalculationService).GetMethod("BuildCommutingFactorQuery", BindingFlags.NonPublic | BindingFlags.Static);
        var result = ((IEnumerable)build.Invoke(null, new object[] { settings, parameters, 2025 })).Cast<object>().ToList();
        if (result.Count != 1 || (string)result[0].GetType().GetProperty("Method").GetValue(result[0]) != "汽車")
            throw new Exception("Commuting factors must use PAR002, PAR004, year, and the active linked parameter.");
        var resolve = typeof(EsgCalculationService).GetMethod("ResolveCommutingFactor", BindingFlags.NonPublic | BindingFlags.Static);
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");
        if ((decimal)resolve.Invoke(null, new object[] { new[] { "0.115", "0.1150" }, "汽車" }) != 0.115m)
            throw new Exception("Commuting factor parsing must be invariant and permit identical duplicates.");
        if ((decimal)resolve.Invoke(null, new object[] { new[] { "0" }, "步行" }) != 0m)
            throw new Exception("Zero is a valid commuting factor.");
        foreach (var values in new[] { new string[0], new[] { "bad" }, new[] { "0.1", "0.2" } })
        {
            try { resolve.Invoke(null, new object[] { values, "汽車" }); }
            catch (TargetInvocationException ex) { if (ex.InnerException is EsgConfigurationException) continue; throw; }
            throw new Exception("Missing, invalid, or conflicting commuting factors must be reported.");
        }
        Console.WriteLine("Commuting factor checks passed.");
    }
}
