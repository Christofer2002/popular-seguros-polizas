using Microsoft.AspNetCore.Mvc;
using Poliza.Interfaces;
using Poliza.Models.Catalogo;

namespace Poliza.Controllers
{
    [ApiController]
    [Route("api/poliza/[controller]")]
    public class CatalogoController : ControllerBase
    {
        private readonly ICatalogoService _catalogoService;

        public CatalogoController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet]
        public async Task<ActionResult<ObtenerCatalogoResponseModel>> ObtenerCatalogos()
        {
            var response = await _catalogoService.ObtenerCatalogos();

            if (!response.Exito) return BadRequest(response);

            return Ok(response);
        }
    }
}
