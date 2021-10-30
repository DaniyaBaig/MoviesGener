using Mechanic.Contracts.Repository;
using Mechanic.Contracts.Services;
using Mechanic.Data;
using Mechanic.Data.Helpers;
using Mechanic.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mechanic.API.ModuleInjections
{
    public class ServicesInjection
    {
        readonly IServiceCollection _serviceCollection;
        public ServicesInjection(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public void MapServices()
        {
            _serviceCollection.AddScoped<IConnectionFactory, ConnectionFactory>();
            _serviceCollection.AddScoped<IMechanicService, MechanicService>();
            _serviceCollection.AddScoped<IMechanicRepository, MechanicRepository>();
        }
    }
}
