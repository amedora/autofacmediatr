using AutofacMediatr.Libs.TestUtilities.Csv.Map;
using CsvHelper;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace AutofacMediatr.Libs.TestUtilities.Csv.Loader
{
    public class CsvLoader
    {
        private string _connectionString;

        public CsvLoader(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Load<T>(string csvFilePath)
            where T : ICsvMap, new()
        {
            using (var streamReader = new StreamReader(csvFilePath, Encoding.UTF8))
            using (var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    var sql = new T().InsertSql;
                    var records = csv.GetRecords<T>();

                    conn.Execute(sql, records);
                    tran.Commit();
                }
            }
        }
    }
}
