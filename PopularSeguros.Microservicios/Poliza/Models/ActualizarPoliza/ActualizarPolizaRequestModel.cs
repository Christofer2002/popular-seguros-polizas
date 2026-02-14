using System.ComponentModel.DataAnnotations;

namespace Poliza.Models.ActualizarPoliza
{
    public class ActualizarPolizaRequestModel
    {
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\-]+$", ErrorMessage = "El número de póliza solo puede contener letras, números y guiones.")]
        public string NumeroPoliza { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El tipo de póliza debe ser válido.")]
        public int TipoPolizaId { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[0-9\-]+$", ErrorMessage = "La cédula solo puede contener números y guiones.")]
        public string CedulaAsegurado { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto asegurado debe ser mayor a 0.")]
        public decimal MontoAsegurado { get; set; }

        [Required]
        public DateTime FechaVencimiento { get; set; }

        [Required]
        public DateTime FechaEmision { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El tipo de cobertura debe ser válido.")]
        public int TipoCoberturaId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El estado de la póliza debe ser válido.")]
        public int EstadoPolizaId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "La prima debe ser mayor a 0.")]
        public decimal Prima { get; set; }

        public DateTime Periodo { get; set; }

        public DateTime FechaInclusion { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9ÁÉÍÓÚáéíóúÑñ\s\.,&-]*$", ErrorMessage = "La aseguradora solo puede contener letras, números y caracteres comunes (.,&-).")]
        public string Aseguradora { get; set; } = string.Empty;
    }
}