using AutofacMediatr.Modules.ModuleA.Application.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutofacMediatr.Modules.ModuleA.Application.TodoTasks.GetTodoTasks
{
    internal class GetTodoTasksQueryHandler
        : IQueryHandler<GetTodoTasksQuery, List<TodoTaskDto>>
    {
        public async Task<List<TodoTaskDto>> Handle(GetTodoTasksQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new List<TodoTaskDto> { new TodoTaskDto { Todo = "TODO 1" } });
        }
    }
}
