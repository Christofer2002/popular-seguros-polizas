using Cliente.Interfaces;
using Cliente.Models;
using Cliente.Models.CrearCliente;
using Cliente.Models.ActualizarCliente; 
using Cliente.Models.ObtenerClientes;
using Cliente.Servicios;
using Comun.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cliente.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost("filtros")]
        public async Task<ActionResult<ObtenerClienteResponseModel>> ObtenerClientes([FromBody] PaginacionRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ObtenerClienteResponseModel
                {
                    Exito = false,
                    Mensaje = "Solicitud inválida.",
                    Data = null
                });
            }

            var response = await _clienteService.ObtenerClientes(request);

            if (!response.Exito)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CrearClienteResponseModel>> CrearCliente(
            [FromBody] CrearClienteRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new CrearClienteResponseModel
                {
                    Exito = false,
                    Mensaje = "Solicitud inválida.",
                    Data = null
                });
            }

            var response = await _clienteService.CrearCliente(request);

            if (!response.Exito)
                return BadRequest(response);

            return CreatedAtAction(nameof(CrearCliente), response);
        }

        [HttpPut("{cedula}")]
        public async Task<ActionResult<ActualizarClienteResponseModel>> ActualizarCliente(string cedula, [FromBody] ActualizarClienteRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ActualizarClienteResponseModel
                {
                    Exito = false,
                    Mensaje = "Solicitud inválida.",
                    Data = null
                });
            }

            var response = await _clienteService.ActualizarCliente(cedula, request);

            if (!response.Exito)
                return NotFound(response);

            return Ok(response);
        }

        [HttpDelete("{cedula}")]
        public async Task<ActionResult<ResponseModel<bool>>> EliminarCliente(string cedula)
        {
            var response = await _clienteService.EliminarCliente(cedula);

            if (!response.Exito)
                return NotFound(response);

            return Ok(response);
        }
    }
}
