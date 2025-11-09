using Core.Aplicacion.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aplicacion.CasosUso.Autenticacion
{
    public class ServicioToken : IServicioToken
    {
        private readonly authSettings _configAuth;
        public ServicioToken (IOptions<authSettings> configAuth)
        {
            _configAuth = configAuth.Value;
        }

        public string GenerarToken(string usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configAuth.Key ?? ""));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario),
                new Claim("rol", "usuario"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken
            (
                issuer: _configAuth.Issuer,
                audience: _configAuth.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configAuth.ExpireMinutes)),
                signingCredentials: credenciales
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
