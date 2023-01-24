using AutofacMediatr.BuildingBlocks.Application.DataAccess;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace AutofacMediatr.BuildingBlocks.Infrastructure.DataAccess
{
    public class OracleConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public OracleConnectionFactory(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            if (this._connection == null || this._connection.State != ConnectionState.Open)
            {
                this._connection = new OracleConnection(_connectionString);
                this._connection.Open();
            }

            return this._connection;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public void Dispose()
        {
            if (this._connection != null && this._connection.State == ConnectionState.Open)
            {
                this._connection.Dispose();
            }
        }
    }
}
