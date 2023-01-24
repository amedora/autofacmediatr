using AutofacMediatr.BuildingBlocks.Domain;
using AutofacMediatr.Modules.ModuleA.Domain.TodoTasks.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacMediatr.Modules.ModuleA.Domain.TodoTasks
{
    public class TodoTask : Entity, IAggregateRoot
    {
        public string Todo { get; private set; }

        private TodoTask(string todo)
        {
            Todo = todo;
        }

        public static TodoTask Create(string todo)
        {
            var todoTask = new TodoTask(todo);
            todoTask.AddDomainEvent(new TodoTaskCreatedDomainEvent());

            return todoTask;
        }
    }
}
