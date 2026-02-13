using Poliza.Models.Catalogo;

namespace Poliza.Interfaces
{
    public interface ICatalogoService
    {
        Task<ObtenerCatalogoResponseModel> ObtenerCatalogos();
    }
}
