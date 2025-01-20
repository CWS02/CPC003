using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;

namespace CPC02.Models.DataAccess
{
    public class CommonDAO
    {
        private readonly string _connectionString;

        public CommonDAO()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CPC"].ConnectionString;
        }

        public bool CheckEmployee(string id, List<string> dep)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT MV004, MV001, RIGHT(MV009, 5) AS pwd 
                       FROM TWNCPC..CMSMV 
                       WHERE LEN(MV022) = 0 ";

                if (dep != null && dep.Count > 0)
                {
                    sql += " AND MV004 IN @dep";
                }

                var result = connection.Query(sql, new { dep });

                return result.Any();
            }
        }
        public string CheckDepartment(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT ME002 
                       FROM TWNCPC..CMSME
                       WHERE ME001 = @id";

                var result = connection.QueryFirstOrDefault<string>(sql, new { id });

                return result;
            }
        }


    }
}