using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Helper.JwtManager
{
    /// <summary>
    ///  The Service for creating and validating json web token, refreshing new token.
    /// </summary>
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly IConfiguration _config;
        private readonly UnitOfWork _unitOfWork;

        public JwtTokenManager(IConfiguration configuration, DentalClinicPlatformContext Dbcontext)
        {
            _config = configuration;
            _unitOfWork = new UnitOfWork(Dbcontext);
        }

        // Generate Access JSON Token that last in a short amount of time.
        // This is generally used for authenticating users in the API endpoint (api/auth/login endpoint)
        public string GenerateAccessToken(User user, int duration = 10)
        {
            string TokenKey = _config.GetValue<string>("JWT:Key")!;
            string Issuer = _config.GetValue<string>("JWT:Issuer")!;


            var userStatus = _unitOfWork.StatusRepository.GetById(user.UserId);
            var userRole = _unitOfWork.RoleRepository.GetById(user.Role);

            var claims = new ClaimsIdentity(new[]
            {
                new Claim("id", user.UserId.ToString()),
                new Claim("username", user.Username),
                new Claim("email", user.Email),
                new Claim("role", userRole!.RoleName),
                new Claim("status",userStatus!.StatusName),
            });

            var TokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor TokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Issuer = Issuer,
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(duration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey)), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = TokenHandler.CreateToken(TokenDescriptor);

            return TokenHandler.WriteToken(token);

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
            if (token == null)
            {
                return null;
            }

            var TokenHandler = new JwtSecurityTokenHandler();

            string TokenKey = _config.GetValue<string>("JWT:Key")!;
            string Issuer = _config.GetValue<string>("JWT:Issuer")!;

            var validatior = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey)),
                ClockSkew = TimeSpan.Zero,
            };

            try
            {
                TokenHandler.ValidateToken(token, validatior, out var validatedToken);

                var Token = (JwtSecurityToken)validatedToken;

                int userId = int.Parse(Token.Claims.First(x => x.Type == "id").Value);

                return _unitOfWork.UserRepository.GetById(userId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
