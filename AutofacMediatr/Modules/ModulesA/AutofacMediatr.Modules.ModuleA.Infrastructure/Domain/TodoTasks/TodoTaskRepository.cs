using AutofacMediatr.Modules.ModuleA.Domain.TodoTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacMediatr.Modules.ModuleA.Infrastructure.Domain.TodoTasks
{
    public class TodoTaskRepository : ITodoTaskRepository
    {
        private readonly ModuleAUnitOfWork _unitOfWork;

        public TodoTaskRepository(ModuleAUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SaveAsync(TodoTask todoTask)
        {
            // await _unitOfWork.Connection.ExecuteAsync(sql, dataModel);
            await _unitOfWork.DispatchDomainEvents(todoTask);
        }
    }
}
