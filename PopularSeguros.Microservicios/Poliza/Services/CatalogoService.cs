using Microsoft.EntityFrameworkCore;
using Poliza.Data;
using Poliza.Interfaces;
using Poliza.Models.Catalogo;

namespace Poliza.Services
{
    public class CatalogoService : ICatalogoService
    {
        private readonly CatalogoDbContext _context;

        public CatalogoService(CatalogoDbContext context)
        {
            _context = context;
        }

        public async Task<ObtenerCatalogoResponseModel> ObtenerCatalogos()
        {
            try
            {
                var tiposPoliza = await _context.TipoPolizaTable.ToListAsync();
                var estadosPoliza = await _context.EstadoPolizaTable.ToListAsync();
                var tiposCoberturas = await _context.TipoCoberturaTable.ToListAsync();

                var catalogo = new CatalogoResponseModel
                {
                    TiposPoliza = tiposPoliza,
                    EstadosPoliza = estadosPoliza,
                    TiposCoberturas = tiposCoberturas
                };

                return new ObtenerCatalogoResponseModel
                {
                    Exito = true,
                    Mensaje = "Catálogos obtenidos correctamente.",
                    Data = catalogo
                };
            }
            catch (Exception ex)
            {
                return new ObtenerCatalogoResponseModel
                {
                    Exito = false,
                    Mensaje = $"Error al obtener los catálogos: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}
