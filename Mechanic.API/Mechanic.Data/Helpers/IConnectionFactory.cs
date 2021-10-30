using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mechanic.Data.Helpers
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
