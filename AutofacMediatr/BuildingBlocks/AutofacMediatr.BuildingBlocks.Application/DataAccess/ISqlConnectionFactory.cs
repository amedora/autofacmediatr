using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AutofacMediatr.BuildingBlocks.Application.DataAccess
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();

        string GetConnectionString();
    }
}
