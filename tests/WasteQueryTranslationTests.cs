using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using CPC02.Models;
using CPC02.Services;

internal static class WasteQueryTranslationTests
{
    public static void Run()
    {
        // Compile SQL Server expressions offline; never open a database connection.
        Database.SetInitializer<DbContext>(null);
        var builder = new DbModelBuilder();
        builder.Entity<WASTES>();
        builder.Entity<SGS_ParameterSetting>();
        builder.Entity<SGS_Parameter>();
        var model = builder.Build(new DbProviderInfo("System.Data.SqlClient", "2012")).Compile();
        using (var connection = new SqlConnection("Server=localhost;Database=OfflineEsgTest;Integrated Security=true"))
        using (var db = new DbContext(connection, model, false))
        {
            var commutingQuery = (IQueryable)typeof(EsgCalculationService)
                .GetMethod("BuildCommutingFactorQuery", BindingFlags.NonPublic | BindingFlags.Static)
                .Invoke(null, new object[] { db.Set<SGS_ParameterSetting>().AsNoTracking(), db.Set<SGS_Parameter>().AsNoTracking(), 2025 });
            if (string.IsNullOrEmpty(commutingQuery.ToString())) throw new Exception("Commuting query did not translate.");
            var buildQuery = typeof(EsgCalculationService).GetMethod("BuildWasteSummaryQuery", BindingFlags.NonPublic | BindingFlags.Static);
            foreach (var method in new[] { "全部", "掩埋", "焚化處理" })
            {
                var query = (IQueryable<WasteSummaryViewModel>)buildQuery.Invoke(null, new object[]
                {
                    db.Set<WASTES>().AsNoTracking(),
                    db.Set<SGS_ParameterSetting>().AsNoTracking(),
                    db.Set<SGS_Parameter>().AsNoTracking(),
                    new SGS_Search { category = "waste", year = "2026", methods = method }
                });
                // ToString runs EF's expression rewriting, which used to fail on
                // AsNoTracking inside the correlated coefficient subquery.
                if (string.IsNullOrEmpty(query.ToString())) throw new Exception("Waste query did not translate.");
                if (connection.State != ConnectionState.Closed) throw new Exception("Offline test opened a connection.");
            }
        }
        Console.WriteLine("Waste query translation checks passed (all treatment filters, no database connection).");
    }
}
