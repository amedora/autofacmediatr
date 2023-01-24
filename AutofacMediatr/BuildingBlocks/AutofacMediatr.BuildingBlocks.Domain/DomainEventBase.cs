using System;
using System.Collections.Generic;
using System.Text;

namespace AutofacMediatr.BuildingBlocks.Domain
{
    public class DomainEventBase : IDomainEvent
    {
        public Guid Id { get; }

        public DateTime OccurredOn { get; }

        public DomainEventBase()
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.Now;
        }
    }
}
