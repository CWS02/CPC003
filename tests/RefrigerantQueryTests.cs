using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using CPC02.Models;
using CPC02.Services;

internal static class RefrigerantQueryTests
{
    private static IQueryable<ColdCoalViewModel> Build(IQueryable<ColdCoal> rows, string factory, int year)
    {
        return (IQueryable<ColdCoalViewModel>)typeof(EsgCalculationService)
            .GetMethod("BuildRefrigerantQuery", BindingFlags.NonPublic | BindingFlags.Static)
            .Invoke(null, new object[] { rows, factory, year });
    }
    public static void Run()
    {
        var rows = new[]
        {
            new ColdCoal { Year = 2024, CC010 = "南科", CC007 = "R-134A", CC012 = 1m },
            new ColdCoal { Year = 2025, CC010 = "南科", CC007 = "R-134A", CC012 = 2m },
            new ColdCoal { Year = 2025, CC010 = "樹谷", CC007 = "R-410A", CC012 = 3m },
            new ColdCoal { Year = 2025, Status = 1, CC010 = "南科", CC007 = "R-134A", CC012 = 500m },
            new ColdCoal { Year = 2025, Status = 2, CC010 = "樹谷", CC007 = "R-410A", CC012 = 600m },
            new ColdCoal { Year = null, CC010 = "南科", CC007 = "R-134A", CC012 = 100m },
            new ColdCoal { Year = 2025, CC010 = "南科", CC007 = "R-22", CC012 = 1000m }
        }.AsQueryable();
        if (Build(rows, "全部", 2025).Sum(x => x.SumCC012) != 5m || Build(rows, "全部", 2025).Count() != 2)
            throw new Exception("Refrigerant report must aggregate only the selected year and supported gases across factories.");
        if (Build(rows, "南科", 2025).Single().SumCC012 != 2m || Build(rows, "全部", 2024).Single().SumCC012 != 1m)
            throw new Exception("Refrigerant year and factory filters must remain independent.");
        Database.SetInitializer<DbContext>(null);
        var builder = new DbModelBuilder();
        builder.Entity<ColdCoal>();
        var model = builder.Build(new DbProviderInfo("System.Data.SqlClient", "2012")).Compile();
        using (var db = new DbContext(new SqlConnection("Server=localhost;Database=OfflineEsgTest;Integrated Security=true"), model, true))
        {
            var queryText = Build(db.Set<ColdCoal>().AsNoTracking(), "全部", 2025).ToString();
            if (!queryText.Contains("[Year]") || !queryText.Contains("[Status]"))
                throw new Exception("EF must translate refrigerant year filtering to the mapped Year column.");
        }
        Console.WriteLine("Refrigerant year and status filter checks passed.");
    }
}
