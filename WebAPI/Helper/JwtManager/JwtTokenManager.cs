using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebAPI.Helper.JwtManager
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly IConfiguration _config;;

        public JwtTokenManager(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string GenerateAccessToken(User user, int duration = 10)
        {
            string TokenKey = _config.GetValue<string>("JWT:Key")!;
            string Issuer = _config.GetValue<string>("JWT:Issuer")!;

            ClaimsIdentity claims = new ClaimsIdentity( new[]
            {
                new Claim("username", ""), 
                new Claim("email", ""),
                new Claim("role", ""),
                new Claim("status",user.Status),
            });

            TokenHandler TokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor TokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject: 
            };

        public string GenerateRefreshToken(User user, int duration = 60)
        {
            throw new NotImplementedException();
        }

        public AuthenticationToken GenerateTokens(User user, int accessDuration = 10, int refreshDuration = 60)
        {
            throw new NotImplementedException();
        }

        public User? ValidateAccessToken(string token)
        {
            throw new NotImplementedException();
        }

        public string GenerateRefreshToken(User user, int duration = 60)
        {
            throw new NotImplementedException();
        }

        public AuthenticationToken GenerateTokens(User user, int accessDuration = 10, int refreshDuration = 60)
        {
            throw new NotImplementedException();
        }

        public User? ValidateAccessToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
