using Cliente.Models;
using Cliente.Models.ActualizarCliente;
using Cliente.Models.ObtenerClientes;
using Cliente.Models.CrearCliente;
using Cliente.Models.BuscarCliente;
using Comun.Models;

namespace Cliente.Interfaces
{
    public interface IClienteService
    {
        Task<ObtenerClienteResponseModel> ObtenerClientes(ObtenerClientesRequestModel request);
        Task<BuscarClienteResponseModel> BuscarClientePorCedula(string cedula);
        Task<CrearClienteResponseModel> CrearCliente(CrearClienteRequestModel request);
        Task<ActualizarClienteResponseModel> ActualizarCliente(string cedula, ActualizarClienteRequestModel request);
        Task<ResponseModel<bool>> EliminarCliente(string cedula);
    }
}
