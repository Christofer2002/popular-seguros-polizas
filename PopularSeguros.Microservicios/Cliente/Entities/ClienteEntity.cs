using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Cliente.Entities
{
    [Table("ClienteTable", Schema = "ClienteSchema")]
    public class ClienteEntity
    {
        [Key]
        [Required]
        [StringLength(20)]
        [Column("CedulaAsegurado")]
        [RegularExpression(@"^[0-9\-]+$", ErrorMessage = "La cédula solo puede contener números y guiones.")]
        [Display(Order = 1)]
        public string CedulaAsegurado { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-ZÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El nombre solo puede contener letras.")]
        [Display(Order = 2)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-ZÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El primer apellido solo puede contener letras.")]
        [Display(Order = 3)]
        public string PrimerApellido { get; set; } = string.Empty;

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-ZÁÉÍÓÚáéíóúÑñ\s]*$", ErrorMessage = "El segundo apellido solo puede contener letras.")]
        [Display(Order = 4)]
        public string? SegundoApellido { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Order = 5)]
        public string TipoPersona { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "date")]
        [Display(Order = 6)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        [Display(Order = 7)]
        public bool EstaEliminado { get; set; } = false;
    }
}
