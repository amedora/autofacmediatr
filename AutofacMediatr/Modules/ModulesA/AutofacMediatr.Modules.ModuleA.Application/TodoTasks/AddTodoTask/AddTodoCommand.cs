using AutofacMediatr.Modules.ModuleA.Application.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacMediatr.Modules.ModuleA.Application.TodoTasks.AddTodoTask
{
    public class AddTodoCommand : CommandBase
    {
        public string Todo { get; set; }

        public AddTodoCommand(string todo)
        {
            Todo = todo;
        }
    }
}
