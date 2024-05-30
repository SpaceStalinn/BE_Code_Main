using Microsoft.AspNetCore.Authentication;
using Repositories.Models;
using System.Text.Json.Nodes;

namespace WebAPI.Services.JwtManager
{
    public interface IJwtTokenManager
    {
        string GenerateAccessToken(User user, int duration = 10);

        string GenerateRefreshToken(User user, int duration = 60);

        User? ValidateAccessToken(string? token);

        User? ValidateAccessToken(string? token, string[] roles);

        AuthenticationToken GenerateTokens(User user, int accessDuration = 10, int refreshDuration = 60);

    }
}
