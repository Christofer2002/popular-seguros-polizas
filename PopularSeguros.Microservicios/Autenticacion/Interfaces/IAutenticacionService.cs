using Autenticacion.Models.Login;
using Comun.Models;

namespace Autenticacion.Interfaces
{
    public interface IAutenticacionService
    {
        Task<LoginResponseModel> Login(LoginRequestModel request);
        Task<ResponseModel<bool>> VerificarDisponibilidadUsuario(string nombreUsuario);
    }
}
