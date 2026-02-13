using System.ComponentModel.DataAnnotations;

namespace Poliza.Models.CrearPoliza
{
    public class CrearPolizaRequestModel
    {
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\-]+$", ErrorMessage = "El número de póliza solo puede contener letras, números y guiones.")]
        public string NumeroPoliza { get; set; } = string.Empty;

        [Required]
        public int TipoPolizaId { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[0-9\-]+$", ErrorMessage = "La cédula solo puede contener números y guiones.")]
        public string CedulaAsegurado { get; set; } = string.Empty;

        [Required]
        public decimal MontoAsegurado { get; set; }

        [Required]
        public DateTime FechaVencimiento { get; set; }

        [Required]
        public DateTime FechaEmision { get; set; }

        public int TipoCoberturaId { get; set; }

        public int EstadoPolizaId { get; set; }

        public decimal Prima { get; set; }

        public DateTime Periodo { get; set; }

        public DateTime FechaInclusion { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9ÁÉÍÓÚáéíóúÑñ\s\.,&-]*$", ErrorMessage = "La aseguradora solo puede contener letras, números y caracteres comunes (.,&-).")]
        public string Aseguradora { get; set; } = string.Empty;
    }
}