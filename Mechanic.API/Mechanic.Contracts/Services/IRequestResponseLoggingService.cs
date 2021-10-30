using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic.Contracts.Services
{
    public interface IRequestResponseLoggingService
    {
        Task<int> ResponseRequestLoggingGetMethod(string request, string response, string ipAddress);
        Task<int> ResponseRequestLoggingPostMethod(string request, string response, string ipAddress);
        Task<int> ResponseRequestLoggingPutMethod(string request, string response, string ipAddress);
    }
}
