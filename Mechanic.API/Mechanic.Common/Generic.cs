using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Mechanic.Common
{
    public class Generic : IGeneric
    {
        private readonly IActionContextAccessor _actionContext;

        public Generic(IActionContextAccessor actionContext)
        {
            _actionContext = actionContext;

        }
        public string GetUserIpAddress()
        {
            return _actionContext.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
