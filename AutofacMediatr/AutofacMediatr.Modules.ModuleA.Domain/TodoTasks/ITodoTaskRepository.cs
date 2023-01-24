using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutofacMediatr.Modules.ModuleA.Domain.TodoTasks
{
    public interface ITodoTaskRepository
    {
        Task SaveAsync(TodoTask todoTask);
    }
}
