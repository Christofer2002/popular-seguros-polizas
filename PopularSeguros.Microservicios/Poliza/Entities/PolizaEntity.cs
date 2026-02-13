using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Poliza.Entities.Catalogo;

namespace Poliza.Entities
{
    [Table("PolizaTable", Schema = "PolizaSchema")]
    public class PolizaEntity
    {
        [Key]
        [Required]
        [Display(Order = 1)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\-]+$", ErrorMessage = "El número de póliza solo puede contener letras, números y guiones.")]
        [Display(Order = 2)]
        public string NumeroPoliza { get; set; } = string.Empty;

        [Required]
        [Display(Order = 3)]
        public int TipoPolizaId { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Order = 4)]
        [RegularExpression(@"^[0-9\-]+$", ErrorMessage = "La cédula solo puede contener números y guiones.")]
        public string CedulaAsegurado { get; set; } = string.Empty;

        [Required]
        [Display(Order = 4)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoAsegurado { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [Display(Order = 5)]
        public DateTime FechaVencimiento { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [Display(Order = 6)]
        public DateTime FechaEmision { get; set; }

        [Display(Order = 8)]
        public int TipoCoberturaId { get; set; }

        [Display(Order = 9)]
        public int EstadoPolizaId { get; set; }

        [Display(Order = 10)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Prima { get; set; }

        [Display(Order = 11)]
        public DateTime Periodo { get; set; }

        [Display(Order = 12)]
        public DateTime FechaInclusion { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9ÁÉÍÓÚáéíóúÑñ\s\.\,&-]*$", ErrorMessage = "La aseguradora solo puede contener letras, números y caracteres comunes (.,&-).")]
        [Display(Order = 13)]
        public string Aseguradora { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "bit")]
        [Display(Order = 14)]
        public bool EstaEliminado { get; set; } = false;

        [ForeignKey("TipoPolizaId")]
        public TipoPolizaEntity TipoPoliza { get; set; }

        [ForeignKey("EstadoPolizaId")]
        public EstadoPolizaEntity EstadoPoliza { get; set; }

        [ForeignKey("TipoCoberturaId")]
        public TipoCoberturaEntity TipoCobertura { get; set; }
    }
}