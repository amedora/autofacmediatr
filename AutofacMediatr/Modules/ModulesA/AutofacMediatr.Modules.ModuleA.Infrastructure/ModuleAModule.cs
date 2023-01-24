using Autofac;
using AutofacMediatr.Modules.ModuleA.Application.Contracts;
using AutofacMediatr.Modules.ModuleA.Infrastructure.Configuration;
using AutofacMediatr.Modules.ModuleA.Infrastructure.Configuration.Processing;
using MediatR;
using System;
using System.Threading.Tasks;

namespace AutofacMediatr.Modules.ModuleA.Infrastructure
{
    public class ModuleAModule : IModuleAModule
    {
        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            return await CommandsExecutor.Execute(command);
        }

        public async Task ExecuteCommandAsync(ICommand command)
        {
            await CommandsExecutor.Execute(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using (var scope = ModuleACompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}
