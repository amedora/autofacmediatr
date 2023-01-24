using System;
using System.Collections.Generic;
using System.Text;

namespace AutofacMediatr.BuildingBlocks.Domain
{
    public interface IBusinessRule
    {
        bool IsBroken();

        string Message { get; }
    }
}
