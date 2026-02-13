using Comun.Models;
using Poliza.Entities.Catalogo;

namespace Poliza.Models.Catalogo
{
    public class CatalogoResponseModel
    {
        public List<TipoPolizaEntity> TiposPoliza { get; set; } = new();
        public List<EstadoPolizaEntity> EstadosPoliza { get; set; } = new();
        public List<TipoCoberturaEntity> TiposCoberturas { get; set; } = new();
    }

    public class ObtenerCatalogoResponseModel : ResponseModel<CatalogoResponseModel>
    {
    }
}
