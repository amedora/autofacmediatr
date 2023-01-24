using AutofacMediatr.Modules.ModuleA.Application.Contracts;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutofacMediatr.Modules.ModuleA.Infrastructure.Configuration.Processing
{
    public class TransactionBehavior<T, TResponse> : IPipelineBehavior<T, TResponse>
            where T : ICommand<TResponse>
    {
        private readonly ModuleAUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public TransactionBehavior(ModuleAUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TResponse> Handle(T command, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);

            try
            {
                if (_unitOfWork.HasActiveTransaction)
                {
                    // すでにトランザクションが開始されているのでそのまま次のアクションを実行する。
                    return await next();
                }

                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    _logger.Information("DBトランザクションを開始します。{Command}", command.GetType().Name);

                    response = await next();

                    _logger.Information("DBトランザクションをコミットします。{Command}", command.GetType().Name);

                    _unitOfWork.CommitTransaction(transaction);
                }

                return response;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBackTransaction();
                _logger.Warning(ex, "DBトランザクションをロールバックしました。{Command}", command.GetType().Name);
                throw;
            }
        }
    }
}
