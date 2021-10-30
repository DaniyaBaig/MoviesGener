using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mechanic.Common
{
    public class UrlRequestCultureProvider : RequestCultureProvider
    {
        private static readonly Regex LocalePattern = new Regex(@"^[a-z]{2}(-[a-z]{2,4})?$",
                                                            RegexOptions.IgnoreCase);

        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            var culture = Convert.ToString(httpContext.Request.Headers["Culture"]);
            if (!string.IsNullOrEmpty(culture) && LocalePattern.IsMatch(culture))
                return Task.FromResult(new ProviderCultureResult(culture));
            else
                return Task.FromResult<ProviderCultureResult>(null);
        }
    }
}
