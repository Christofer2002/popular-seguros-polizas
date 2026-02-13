using System.ComponentModel.DataAnnotations;

namespace Cliente.Models.ActualizarCliente
{
    public class ActualizarClienteRequestModel
    {
        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[0-9\-]+$", ErrorMessage = "La cédula solo puede contener números y guiones.")]
        public string CedulaAsegurado { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-ZÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El nombre solo puede contener letras.")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-ZÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El primer apellido solo puede contener letras.")]
        public string PrimerApellido { get; set; } = string.Empty;

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-ZÁÉÍÓÚáéíóúÑñ\s]*$", ErrorMessage = "El segundo apellido solo puede contener letras.")]
        public string? SegundoApellido { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoPersona { get; set; } = string.Empty;

        [Required]
        public DateTime FechaNacimiento { get; set; }
    }
}
