using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Mechanic.Common;

namespace Mechanic.Data.Helpers
{
    public class ConnectionFactory : IConnectionFactory
    {
        readonly ConnectionStrings _connectionStrings;
        public ConnectionFactory(ConnectionStrings connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }
        public IDbConnection GetConnection()
        {
            var factory = DbProviderFactories.GetFactory(_connectionStrings.MechanicApp.ProviderName);
            var conn = factory.CreateConnection();
            conn.ConnectionString = _connectionStrings.MechanicApp.ConnectionString;
            conn.Open();
            return conn;
        }

        
    }
}
