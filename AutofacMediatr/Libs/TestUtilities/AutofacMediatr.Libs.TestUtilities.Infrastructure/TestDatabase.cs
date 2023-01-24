using AutofacMediatr.Libs.TestUtilities.Csv.Loader;
using AutofacMediatr.Libs.TestUtilities.Csv.Map;
using Oracle.ManagedDataAccess.Client;
using Respawn;
using System;
using System.Threading.Tasks;

namespace AutofacMediatr.Libs.TestUtilities.Infrastructure
{
    public class TestDatabase
    {
        private readonly string _connectionString;
        private readonly Checkpoint _checkPoint;
        private readonly CsvLoader _loader;
        private readonly string[] _schemas;

        public TestDatabase(string connectionString, string schema)
        {
            _connectionString = connectionString;
            _schemas = new string[] { schema };
            _checkPoint = CreateCheckPoint();
            _loader = new CsvLoader(connectionString);
        }

        public void Load<T>(string csvFilePath)
            where T : ICsvMap, new()
        {
            _loader.Load<T>(csvFilePath);
        }

        public async Task ResetAsync()
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();
                await _checkPoint.Reset(connection);
            }
        }

        private Checkpoint CreateCheckPoint()
        {
            return new Checkpoint
            {
                SchemasToInclude = _schemas,
                //TablesToIgnore = new[]
                //{

                //},
                DbAdapter = DbAdapter.Oracle
            };
        }
    }
}
