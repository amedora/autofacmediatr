using System;
using System.Collections.Generic;
using System.Text;

namespace AutofacMediatr.BuildingBlocks.Domain
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {
    }
}
