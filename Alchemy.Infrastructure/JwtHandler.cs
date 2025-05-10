using Alchemy.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;

namespace Alchemy.Infrastructure
{
    public class JwtHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _googleSection;
        private readonly IConfigurationSection _jwtSection;
        private readonly UserManager<User> _userManager;

        public JwtHandler(IConfiguration configuration, UserManager<User> userManager, IConfigurationSection googleSection, IConfigurationSection jwtSection)
        {
            _configuration = configuration;
            _jwtSection = _configuration.GetSection("Jwt");
            _userManager = userManager;
            _googleSection = _configuration.GetSection("Google");
        }
        
        public class ExternalLoginRequest
        {
            public string? Provider { get; set; }
            public string? IdToken { get; set; }
        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalLoginRequest login)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = [_googleSection.GetSection("ClientId").Value]
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(login.IdToken, settings);

                return payload;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<string> GenerateToken(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSection.GetSection("SecretKey").Value!);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName!),
                new(ClaimTypes.Email, user.Email!)
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCred, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSection.GetSection("Issuer").Value,
                audience: _jwtSection.GetSection("Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSection.GetSection("ExpiryInMinutes").Value)),
                signingCredentials: signingCred
            );

            return tokenOptions;
        }
    }
}
