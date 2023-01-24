using AutofacMediatr.Modules.ModuleA.Application.Configuration;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutofacMediatr.Modules.ModuleA.Application.TodoTasks.AddTodoTask
{
    internal class AddTodoCommandHandler : ICommandHandler<AddTodoCommand>
    {
        public Task<Unit> Handle(AddTodoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
