using AutofacMediatr.Modules.ModuleA.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacMediatr.Modules.ModuleA.Application.Configuration
{
    public abstract class QueryBase<TResult> : IQuery<TResult>
    {
        protected QueryBase()
        {
            this.Id = Guid.NewGuid();
        }

        protected QueryBase(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}
