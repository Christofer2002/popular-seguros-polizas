using Autenticacion.Entities;
using Comun.Models;

namespace Autenticacion.Models.Login
{
    public class LoginResponseModel : ResponseModel<UsuarioLoginDto>
    {
    }

    public class UsuarioLoginDto
    {
        public Guid Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaUltimoLogin { get; set; }
    }
}
