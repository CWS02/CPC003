using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using CPC02.Models;
using CPC02.Services;

internal static class TrafficQueryTests
{
    private static IQueryable Build(string name, params object[] args)
    {
        return (IQueryable)typeof(EsgCalculationService)
            .GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, args);
    }

    private static decimal Amount(IQueryable query)
    {
        return ((IEnumerable)query).Cast<object>().Sum(x => (decimal)x.GetType().GetProperty("Amount").GetValue(x));
    }

    public static void Run()
    {
        var headers = new[] { new ACPTA { TA001 = "A", TA002 = "1" } }.AsQueryable();
        var details = new[]
        {
            new ACPTB { TB001 = "A", TB002 = "1", TB008 = "20250101", UDF01 = "高鐵", UDF06 = 10m },
            new ACPTB { TB001 = "A", TB002 = "1", TB008 = "20250201", UDF01 = "高鐵", UDF06 = 5m },
            new ACPTB { TB001 = "A", TB002 = "1", TB008 = "20251231", UDF01 = "捷運-台北", UDF06 = 20m },
            new ACPTB { TB001 = "A", TB002 = "1", TB008 = "20250101", UDF01 = "公務車", UDF06 = 1000m },
            new ACPTB { TB001 = "A", TB002 = "1", TB008 = "20240101", UDF01 = "高鐵", UDF06 = 2000m },
            new ACPTB { TB001 = "A", TB002 = "missing", TB008 = "20250101", UDF01 = "高鐵", UDF06 = 3000m }
        }.AsQueryable();
        if (Amount(Build("BuildTrafficExpenseQuery", headers, details, "全部", "2025")) != 35m)
            throw new Exception("Traffic expenses must filter year, mode, and matching document before aggregation.");
        if (Amount(Build("BuildTrafficExpenseQuery", headers, details, "高鐵", "2025")) != 15m)
            throw new Exception("Single transport mode must retain the same filtering and totals.");
        var payments = new[]
        {
            new PCMTG { TG013 = "20250101", UDF01 = "飛機", UDF06 = 30m },
            new PCMTG { TG013 = "20250101", UDF01 = "地鐵-日本", UDF06 = 40m },
            new PCMTG { TG013 = "20250101", UDF01 = "採購", UDF06 = 4000m },
            new PCMTG { TG013 = "20260101", UDF01 = "飛機", UDF06 = 5000m }
        }.AsQueryable();
        if (Amount(Build("BuildTrafficPaymentQuery", payments, "全部", "2025")) != 70m)
            throw new Exception("Traffic payments must exclude unrelated modes and years.");

        Database.SetInitializer<DbContext>(null);
        var builder = new DbModelBuilder();
        builder.Entity<ACPTA>(); builder.Entity<ACPTB>(); builder.Entity<PCMTG>();
        var model = builder.Build(new DbProviderInfo("System.Data.SqlClient", "2012")).Compile();
        using (var connection = new SqlConnection("Server=localhost;Database=OfflineEsgTest;Integrated Security=true"))
        using (var db = new DbContext(connection, model, false))
        {
            var expenseText = Build("BuildTrafficExpenseQuery", db.Set<ACPTA>().AsNoTracking(),
                db.Set<ACPTB>().AsNoTracking(), "全部", "2025").ToString();
            var paymentText = Build("BuildTrafficPaymentQuery", db.Set<PCMTG>().AsNoTracking(), "全部", "2025").ToString();
            foreach (var text in new[] { expenseText, paymentText })
            {
                foreach (var mode in new[] { "高鐵", "飛機", "計程車", "捷運", "台灣火車", "地鐵", "私車公用" })
                    if (!text.Contains(mode)) throw new Exception("All traffic modes must be filtered by the provider before aggregation.");
                if (!text.Contains("GROUP BY") || text.Contains("LTRIM") || text.Contains("RTRIM"))
                    throw new Exception("Traffic aggregation must run in the provider without trimming indexed join keys.");
            }
            if (connection.State != ConnectionState.Closed) throw new Exception("Offline traffic test opened a connection.");
        }
        Console.WriteLine("Traffic query checks passed (early filters, totals, and offline EF translation).");
    }
}
