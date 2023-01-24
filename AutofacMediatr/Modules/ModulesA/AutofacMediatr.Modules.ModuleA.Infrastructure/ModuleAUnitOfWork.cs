using AutofacMediatr.BuildingBlocks.Domain;
using MediatR;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacMediatr.Modules.ModuleA.Infrastructure
{
    public class ModuleAUnitOfWork
    {
        private IDbConnection _connection;
        private readonly string _connectionString;
        private IDbTransaction _currentTransaction;
        private readonly IMediator _mediator;

        public bool HasActiveTransaction => _currentTransaction != null;

        public IDbConnection Connection
        {
            get
            {
                if (_currentTransaction?.Connection != null)
                    return _currentTransaction.Connection;

                if (_connection == null)
                    _connection = new OracleConnection(_connectionString);

                if (string.IsNullOrWhiteSpace(_connection.ConnectionString))
                    _connection.ConnectionString = _connectionString;

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                return _connection;
            }
        }

        public ModuleAUnitOfWork(string connectionString, IMediator mediator)
        {
            _connectionString = connectionString;
            _mediator = mediator;
        }

        public IDbTransaction BeginTransaction()
        {
            if (HasActiveTransaction)
            {
                throw new InvalidOperationException("トランザクションの中でトランザクションを開始しようとしました。");
            }

            _currentTransaction = Connection.BeginTransaction(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public void CommitTransaction(IDbTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"現在のトランザクションと異なるトランザクションをコミットしようとしました。");

            try
            {
                // NOTE: 将来ドメインイベントを実装する場合はこのコミットの前後でイベントを発行する。
                _currentTransaction.Commit();
            }
            catch
            {
                RollBackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollBackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task DispatchDomainEvents(Entity entity)
        {
            var events = entity.DomainEvents?.ToList();
            if (events == null || events.Count() < 1)
                return;

            entity.ClearDomainEvents();

            foreach (var @event in events)
            {
                await _mediator.Publish(@event);
            }
        }
    }
}
