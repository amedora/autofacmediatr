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
    internal class CommandLoggingBehavior<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        private readonly ILogger _logger;

        public CommandLoggingBehavior(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                _logger.Information("コマンドを実行します。{Command}", command.GetType().Name);

                var result = await next();

                _logger.Information("コマンドの実行が成功しました。{Command}", command.GetType().Name);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "コマンドの実行で例外が発生しました。{Command}", command.GetType().Name);
                throw;
            }
        }
    }
}
