using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Helper.AuthorizationPolicy
{
    public class JwtTokenAuthorization : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            // Skip all endpoint methods that allow anonymous access ([AllowAnonymous] attribute)
            var allowAnoymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

            if (allowAnoymous)
            {
                return;
            }
        }
    }
}
