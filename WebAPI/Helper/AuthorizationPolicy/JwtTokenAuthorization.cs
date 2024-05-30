using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repositories.Models;
using WebAPI.Services.JwtManager;

namespace WebAPI.Helper.AuthorizationPolicy
{
    [AttributeUsage(AttributeTargets.Method)]
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

            object? userInfo = context.HttpContext.Items["UserInfo"];

            if (userInfo == null)
            {
                context.Result = new JsonResult(new {messsage = "Unauthorized", time = DateTime.UtcNow }) 
                { 
                    StatusCode = StatusCodes.Status401Unauthorized,  
                    ContentType = "application/json",
                };
                return;
            }
        }
    }
}
