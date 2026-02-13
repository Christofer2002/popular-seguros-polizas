using System.ComponentModel.DataAnnotations;

namespace Autenticacion.Models.Login
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 100 caracteres.")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener mínimo 6 caracteres.")]
        public string Contraseña { get; set; } = string.Empty;
    }
}
