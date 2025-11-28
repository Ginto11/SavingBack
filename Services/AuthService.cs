using Microsoft.IdentityModel.Tokens;
using SavingBack.Dtos;
using SavingBack.Models;
using SavingBack.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SavingBack.Services
{
    public class AuthService
    {
        private readonly IConfiguration config;
        private readonly Utilidad utilities;
        public AuthService(IConfiguration config, Utilidad utilities)
        {
            this.config = config;
            this.utilities = utilities;
        }

        public string GenerarToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("IdUsuario", usuario.Id.ToString()),
                new Claim("Nombre", usuario.PrimerNombre),
                new Claim("Rol", usuario.Rol!)
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: credenciales
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public JwtDataDto? ObtenerDeToken(string token)
        {
            var manejador = new JwtSecurityTokenHandler();

            if (!manejador.CanReadToken(token))
                return null;

            var claimsUsuario = manejador.ReadJwtToken(token).Claims;

            return new JwtDataDto
            {
                Id = claimsUsuario.FirstOrDefault(c => c.Type == "Id")?.Value,
                NombreCompleto = claimsUsuario.FirstOrDefault(c => c.Type == "Nombre")?.Value,
                Rol = claimsUsuario.FirstOrDefault(c => c.Type == "Rol")?.Value
            };

        }


        public ClaimsPrincipal? ValidarToken(string token)
        {
            byte[] bytesDeLLaveSecreta = Encoding.UTF8.GetBytes(config["Jwt:Key"]!);

            var manejadorToken = new JwtSecurityTokenHandler();

            var parametrosAValidar = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(bytesDeLLaveSecreta),

                ValidateAudience = true,
                ValidateIssuer = true,

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero

            };

            try
            {
                var claimPrincipal = manejadorToken.ValidateToken(token, parametrosAValidar, out SecurityToken validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;

                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Token invalido");

                return claimPrincipal;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
