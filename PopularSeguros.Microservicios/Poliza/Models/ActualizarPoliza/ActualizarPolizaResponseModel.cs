using Poliza.Entities;
using Comun.Models;

namespace Poliza.Models.ActualizarPoliza
{
    public class ActualizarPolizaResponseModel : ResponseModel<PolizaEntity>
    {
        public Guid? Id { get; set; }
    }
}