using AutofacMediatr.Modules.ModuleA.Domain.TodoTasks.Events;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutofacMediatr.Modules.ModuleA.Application.TodoTasks.DomainEventHandlers
{
    internal class TodoTaskCreatedDomainEventHandler : INotificationHandler<TodoTaskCreatedDomainEvent>
    {
        private readonly ILogger _logger;

        public TodoTaskCreatedDomainEventHandler(ILogger logger)
        {
            _logger = logger;
        }

        public Task Handle(TodoTaskCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.Information($"{nameof(TodoTaskCreatedDomainEvent)} has handled.");

            return Task.CompletedTask;
        }
    }
}
