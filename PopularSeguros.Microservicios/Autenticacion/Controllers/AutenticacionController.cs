using Autenticacion.Interfaces;
using Autenticacion.Models.Login;
using Comun.Models;
using Microsoft.AspNetCore.Mvc;

namespace Autenticacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacionController : ControllerBase
    {
        private readonly IAutenticacionService _autenticacionService;

        public AutenticacionController(IAutenticacionService autenticacionService)
        {
            _autenticacionService = autenticacionService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? e.Exception?.Message ?? "Valor inválido" : e.ErrorMessage)
                    .ToList();

                return BadRequest(new LoginResponseModel
                {
                    Exito = false,
                    Mensaje = "Solicitud inválida.",
                    Data = null,
                    Errores = errores
                });
            }

            var response = await _autenticacionService.Login(request);

            if (!response.Exito)
                return Unauthorized(response);

            return Ok(response);
        }

        [HttpGet("verificar-usuario/{nombreUsuario}")]
        public async Task<ActionResult<ResponseModel<bool>>> VerificarDisponibilidadUsuario(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || nombreUsuario.Length < 3 || nombreUsuario.Length > 100)
            {
                return BadRequest(new ResponseModel<bool>
                {
                    Exito = false,
                    Mensaje = "El nombre de usuario debe tener entre 3 y 100 caracteres.",
                    Data = false
                });
            }

            var response = await _autenticacionService.VerificarDisponibilidadUsuario(nombreUsuario);

            if (!response.Exito)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
