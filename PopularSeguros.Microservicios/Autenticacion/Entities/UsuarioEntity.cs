using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Autenticacion.Entities
{
    [Table("UsuarioTable", Schema = "AutenticacionSchema")]
    public class UsuarioEntity
    {
        [Key]
        [Required]
        [Display(Order = 1)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9_\-\.]+$", ErrorMessage = "El nombre de usuario solo puede contener letras, números, guiones, guiones bajos y puntos.")]
        [Display(Order = 2)]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        [Display(Order = 3)]
        public string Contraseña { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El email debe tener un formato válido.")]
        [Display(Order = 4)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "bit")]
        [Display(Order = 5)]
        public bool Activo { get; set; } = true;

        [Required]
        [Column(TypeName = "datetime2")]
        [Display(Order = 6)]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime2")]
        [Display(Order = 7)]
        public DateTime? FechaUltimoLogin { get; set; }
    }
}
