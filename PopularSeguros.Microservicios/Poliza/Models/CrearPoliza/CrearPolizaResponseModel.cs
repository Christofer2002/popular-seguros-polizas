using Poliza.Entities;
using Comun.Models;

namespace Poliza.Models.CrearPoliza
{
    public class CrearPolizaResponseModel : ResponseModel<PolizaEntity>
    {
        public Guid? Id { get; set; }
    }
}