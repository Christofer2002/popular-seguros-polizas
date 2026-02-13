using Comun.Models;
using Poliza.Models;
using Poliza.Models.CrearPoliza;
using Poliza.Models.ObtenerPolizas;

namespace Poliza.Interfaces
{
    public interface IPolizaService
    {
        Task<ObtenerPolizaResponseModel> ObtenerPolizas(ObtenerPolizasRequestModel request);
        Task<CrearPolizaResponseModel> CrearPoliza(CrearPolizaRequestModel request);
        Task<Poliza.Models.ActualizarPoliza.ActualizarPolizaResponseModel> ActualizarPoliza(Guid id, Poliza.Models.ActualizarPoliza.ActualizarPolizaRequestModel request);
        Task<Comun.Models.ResponseModel<bool>> EliminarPoliza(Guid id);
    }
}