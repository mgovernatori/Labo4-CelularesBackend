using CelularesAPI.Models.Usuario;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CelularesAPI.Services
{
    public interface IAuthServices
    {
        string GenerateToken(Usuario usuario);
    }

    public class AuthServices : IAuthServices
    {
        private readonly IConfiguration _configuration;

        public AuthServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Usuario usuario)
        {
            var JWTSettings = _configuration.GetSection("JWTSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings["Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: new[] { new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()) },
                expires: DateTime.UtcNow.AddMinutes(double.Parse(JWTSettings["Expires"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
