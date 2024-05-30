using WebAPI.Services.JwtManager;

namespace WebAPI.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtTokenManager tokenManager)
        {
            string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            // using the Token Manager Service from the Service Collection to validate the token.
            var User = tokenManager.ValidateAccessToken(token);

            if (User != null)
            {
                context.Items["UserInfo"] = User;
            }

            await _next(context);
        }
    }
}
