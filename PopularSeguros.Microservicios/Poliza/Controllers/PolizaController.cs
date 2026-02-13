using Microsoft.AspNetCore.Mvc;
using Poliza.Interfaces;
using Poliza.Models.CrearPoliza;
using Poliza.Models.ObtenerPolizas;
using Comun.Models;
using Poliza.Models.ActualizarPoliza;

namespace Poliza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolizaController : ControllerBase
    {
        private readonly IPolizaService _polizaService;

        public PolizaController(IPolizaService polizaService)
        {
            _polizaService = polizaService;
        }

        [HttpPost("filtros")]
        public async Task<ActionResult<ObtenerPolizaResponseModel>> ObtenerPolizas([FromBody] ObtenerPolizasRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ObtenerPolizaResponseModel { Exito = false, Mensaje = "Solicitud inválida.", Data = null });
            }

            var response = await _polizaService.ObtenerPolizas(request);

            if (!response.Exito) return BadRequest(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CrearPolizaResponseModel>> CrearPoliza([FromBody] CrearPolizaRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new CrearPolizaResponseModel { Exito = false, Mensaje = "Solicitud inválida.", Data = null });
            }

            var response = await _polizaService.CrearPoliza(request);

            if (!response.Exito) return BadRequest(response);

            return CreatedAtAction(nameof(CrearPoliza), response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ActualizarPolizaResponseModel>> ActualizarPoliza(Guid id, [FromBody] Poliza.Models.ActualizarPoliza.ActualizarPolizaRequestModel request)
        {
            if (!ModelState.IsValid) 
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? e.Exception?.Message ?? "Valor inválido" : e.ErrorMessage)
                    .ToList();

                return BadRequest(new ActualizarPolizaResponseModel 
                { 
                    Exito = false, 
                    Mensaje = "Solicitud inválida.", 
                    Data = null,
                    Errores = errores
                });
            }

            var response = await _polizaService.ActualizarPoliza(id, request);

            if (!response.Exito) return NotFound(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Comun.Models.ResponseModel<bool>>> EliminarPoliza(Guid id)
        {
            var response = await _polizaService.EliminarPoliza(id);

            if (!response.Exito) return NotFound(response);

            return Ok(response);
        }
    }
}