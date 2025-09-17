using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using PraktikPortalen.Domain.Entities;
using PraktikPortalen.Application.Security;

namespace PraktikPortalen.Application.Security
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _options;
        private readonly JsonWebTokenHandler _handler = new();

        public TokenService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string CreateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role.ToString()) // "Member" or "Admin"
            };

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_options.ExpiresMinutes),
                SigningCredentials = creds
            };

            return _handler.CreateToken(descriptor);
        }
    }
}