using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIOCaseRU;

namespace Casers.SqlDB
{
    public class SqlDBCaser : FIOCaseRU.StaticMethods.CaserBase
    {
        private readonly string _connCs;
        private readonly string _tableName;
        public SqlDBCaser(string connCS, string tableName)
        {
            _connCs = connCS;
            _tableName = tableName;
        }
        public override string GetCase(string toCase, Gender gender, Case c)
        {
            var sqlQuery = "select Cased from Casing.{0} where Nominative = '{1}' and [Case]={2} and Gender={3}";
            var Query = string.Format(sqlQuery, _tableName, toCase, (byte)c, (byte)gender);
            using (var conn = new SqlConnection(_connCs))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = Query;
                    var res = cmd.ExecuteScalar();
                    return res.ToString();
                }
            }
        }
    }
}
