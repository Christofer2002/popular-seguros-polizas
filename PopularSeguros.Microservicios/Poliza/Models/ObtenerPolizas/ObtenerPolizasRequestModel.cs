using Comun.Models;

namespace Poliza.Models.ObtenerPolizas
{
    public class ObtenerPolizasRequestModel : PaginacionRequestModel
    {
        public string? NumeroPoliza { get; set; }
        public int? TipoPolizaId { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string? CedulaAsegurado { get; set; }
        public string? Nombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
    }
}
