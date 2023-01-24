using AutofacMediatr.Modules.ModuleA.Application.Configuration;
using AutofacMediatr.Modules.ModuleA.Domain.TodoTasks;
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
        private readonly ITodoTaskRepository _todoTaskRepository;

        public AddTodoCommandHandler(ITodoTaskRepository todoTaskRepository)
        {
            _todoTaskRepository = todoTaskRepository;
        }

        public async Task<Unit> Handle(AddTodoCommand request, CancellationToken cancellationToken)
        {
            var todoTask = TodoTask.Create(request.Todo);
            await _todoTaskRepository.SaveAsync(todoTask);

            return Unit.Value;
        }
    }
}
