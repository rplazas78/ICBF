using Core.Aplicacion.CasosUso.Autenticacion;
using Core.DTOs.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace chatBootAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IServicioToken _servicioToken;

        public AuthController(IServicioToken servicioToken)
        {
            _servicioToken = servicioToken;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO request)
        {
            // Validación de usuario básica (puedes reemplazar con DB)
            if (request.Usuario == "icbf" && request.Password == "1234")
            {
                var token = _servicioToken.GenerarToken(request.Usuario);
                return Ok(new { token });
            }

            return Unauthorized(new { mensaje = "Credenciales inválidas" });
        }
    }
}
