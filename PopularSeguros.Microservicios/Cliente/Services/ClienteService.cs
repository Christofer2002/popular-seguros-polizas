using Cliente.Data;
using Cliente.Entities;
using Cliente.Interfaces;
using Cliente.Models;
using Cliente.Models.CrearCliente;
using Cliente.Models.ObtenerClientes;
using Comun.Models;
using Microsoft.EntityFrameworkCore;

namespace Cliente.Servicios
{
    public class ClienteService : IClienteService
    {
        private readonly ClienteDbContext _context;

        public ClienteService(ClienteDbContext context)
        {
            _context = context;
        }

        public async Task<ObtenerClienteResponseModel> ObtenerClientes(ObtenerClientesRequestModel request)
        {
            try
            {
                var query = _context.ClienteTable.Where(c => !c.EstaEliminado);

                if (!string.IsNullOrWhiteSpace(request.CedulaAsegurado))
                {
                    query = query.Where(c => c.CedulaAsegurado.Contains(request.CedulaAsegurado));
                }

                if (!string.IsNullOrWhiteSpace(request.Nombre))
                {
                    query = query.Where(c => c.Nombre.Contains(request.Nombre));
                }

                if (!string.IsNullOrWhiteSpace(request.PrimerApellido))
                {
                    query = query.Where(c => c.PrimerApellido.Contains(request.PrimerApellido));
                }

                if (!string.IsNullOrWhiteSpace(request.SegundoApellido))
                {
                    query = query.Where(c => c.SegundoApellido.Contains(request.SegundoApellido));
                }

                if (!string.IsNullOrWhiteSpace(request.TipoPersona))
                {
                    query = query.Where(c => c.TipoPersona.Contains(request.TipoPersona));
                }

                var totalRegistros = await query.CountAsync();

                int cantidadPaginas = (int)Math.Ceiling(totalRegistros / (double)request.CantidadRegistros);

                var clientes = await query
                    .OrderByDescending(c => c.CedulaAsegurado)
                    .Skip((request.Pagina - 1) * request.CantidadRegistros)
                    .Take(request.CantidadRegistros)
                    .ToListAsync();

                return new ObtenerClienteResponseModel
                {
                    Exito = true,
                    Mensaje = "Clientes obtenidos correctamente.",
                    Data = clientes,
                    Paginacion = new PaginacionModel
                    {
                        PaginaActual = request.Pagina,
                        CantidadRegistros = request.CantidadRegistros,
                        TotalRegistros = totalRegistros,
                        CantidadPaginas = cantidadPaginas
                    }
                };
            }
            catch (Exception ex)
            {
                return new ObtenerClienteResponseModel
                {
                    Exito = false,
                    Mensaje = $"Error al obtener los clientes: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<CrearClienteResponseModel> CrearCliente(CrearClienteRequestModel request)
        {
            try
            {

                var clienteExistente = await _context.ClienteTable
                    .FirstOrDefaultAsync(c => c.CedulaAsegurado == request.CedulaAsegurado);

                if (clienteExistente != null)
                {
                    if (!clienteExistente.EstaEliminado)
                    {
                        return new CrearClienteResponseModel
                        {
                            Exito = false,
                            Mensaje = "Ya existe un cliente con esa cédula.",
                            Data = null
                        };
                    }

                    clienteExistente.Nombre = request.Nombre;
                    clienteExistente.PrimerApellido = request.PrimerApellido;
                    clienteExistente.SegundoApellido = request.SegundoApellido;
                    clienteExistente.TipoPersona = request.TipoPersona;
                    clienteExistente.FechaNacimiento = request.FechaNacimiento;
                    clienteExistente.EstaEliminado = false;

                    _context.ClienteTable.Update(clienteExistente);
                    await _context.SaveChangesAsync();

                    return new CrearClienteResponseModel
                    {
                        Exito = true,
                        Mensaje = "Cliente restaurado y actualizado correctamente.",
                        Data = clienteExistente
                    };
                }

                var cliente = new ClienteEntity
                {
                    CedulaAsegurado = request.CedulaAsegurado,
                    Nombre = request.Nombre,
                    PrimerApellido = request.PrimerApellido,
                    SegundoApellido = request.SegundoApellido,
                    TipoPersona = request.TipoPersona,
                    FechaNacimiento = request.FechaNacimiento
                };

                _context.ClienteTable.Add(cliente);
                await _context.SaveChangesAsync();

                return new CrearClienteResponseModel
                {
                    Exito = true,
                    Mensaje = "Cliente creado correctamente.",
                    Data = cliente
                };
            }
            catch (Exception ex)
            {
                return new CrearClienteResponseModel
                {
                    Exito = false,
                    Mensaje = $"Error al crear el cliente: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<Cliente.Models.ActualizarCliente.ActualizarClienteResponseModel> ActualizarCliente(string cedula, Cliente.Models.ActualizarCliente.ActualizarClienteRequestModel request)
        {
            try
            {
                var cliente = await _context.ClienteTable
                    .FirstOrDefaultAsync(c => c.CedulaAsegurado == cedula && !c.EstaEliminado);

                if (cliente == null)
                {
                    return new Cliente.Models.ActualizarCliente.ActualizarClienteResponseModel
                    {
                        Exito = false,
                        Mensaje = "Cliente no encontrado.",
                        Data = null
                    };
                }

                cliente.Nombre = request.Nombre;
                cliente.PrimerApellido = request.PrimerApellido;
                cliente.SegundoApellido = request.SegundoApellido;
                cliente.TipoPersona = request.TipoPersona;
                cliente.FechaNacimiento = request.FechaNacimiento;

                await _context.SaveChangesAsync();

                return new Cliente.Models.ActualizarCliente.ActualizarClienteResponseModel
                {
                    Exito = true,
                    Mensaje = "Cliente actualizado correctamente.",
                    Data = cliente
                };
            }
            catch (Exception ex)
            {
                return new Cliente.Models.ActualizarCliente.ActualizarClienteResponseModel
                {
                    Exito = false,
                    Mensaje = $"Error al actualizar el cliente: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ResponseModel<bool>> EliminarCliente(string cedula)
        {
            try
            {
                var cliente = await _context.ClienteTable
                    .FirstOrDefaultAsync(c => c.CedulaAsegurado == cedula && !c.EstaEliminado);

                if (cliente == null)
                {
                    return new ResponseModel<bool>
                    {
                        Exito = false,
                        Mensaje = "Cliente no encontrado.",
                        Data = false
                    };
                }

                cliente.EstaEliminado = true;
                _context.ClienteTable.Update(cliente);
                await _context.SaveChangesAsync();

                return new ResponseModel<bool>
                {
                    Exito = true,
                    Mensaje = "Cliente eliminado correctamente.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool>
                {
                    Exito = false,
                    Mensaje = $"Error al eliminar el cliente: {ex.Message}",
                    Data = false
                };
            }
        }
    }
}