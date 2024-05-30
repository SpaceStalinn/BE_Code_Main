using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repositories.Models;
using WebAPI.Services.JwtManager;

namespace WebAPI.Helper.AuthorizationPolicy
{
    [AttributeUsage(AttributeTargets.Method)]
    public class JwtTokenAuthorization : Attribute, IAuthorizationFilter
    {
        private readonly string[] allowedRoles;

        public JwtTokenAuthorization()
        {
            allowedRoles = [];
        }

        public JwtTokenAuthorization(string? Roles)
        {
            if (Roles == null)
            {
                allowedRoles = [];
            }
            else
            {
                allowedRoles = Roles.Split(",");
            }
            
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            // Skip all endpoint methods that allow anonymous access ([AllowAnonymous] attribute)
            var allowAnoymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

            if (allowAnoymous)
            {
                return;
            }

            var requestedFeature = context.HttpContext.RequestServices.GetService<IJwtTokenManager>();

            //object? userInfo = context.HttpContext.Items["UserInfo"];

            var token = context.HttpContext.Request.Headers.Authorization.ToString();

            if (token == null)
            {
                context.Result = new JsonResult(new {messsage = "Unauthorized! User has not login yet.", time = DateTime.UtcNow }) 
                { 
                    StatusCode = StatusCodes.Status401Unauthorized,  
                    ContentType = "application/json",
                };
                return;
            }
            else
            {
                if (requestedFeature!.ValidateAccessToken(token.Split(" ").Last(), allowedRoles) == null)
                {
                    context.Result = new JsonResult(new { messsage = "Unauthorized! Missing permissions", time = DateTime.UtcNow })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        ContentType = "application/json",
                    };
                    return;
                }
            }
        }
    }
}
