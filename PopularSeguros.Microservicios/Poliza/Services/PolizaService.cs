using Microsoft.EntityFrameworkCore;
using Poliza.Data;
using Poliza.Entities;
using Poliza.Interfaces;
using Poliza.Models.CrearPoliza;
using Poliza.Models.ObtenerPolizas;
using Comun.Models;
using System.Text.Json;
using Poliza.Models.Cliente;

namespace Poliza.Services
{
    public class PolizaService : IPolizaService
    {
        private readonly PolizaDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PolizaService(PolizaDbContext context, HttpClient httpClient, IConfiguration configuration)
        {
            _context = context;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ObtenerPolizaResponseModel> ObtenerPolizas(ObtenerPolizasRequestModel request)
        {
            try
            {
                var query = _context.PolizaTable.Where(p => !p.EstaEliminado);

                if (!string.IsNullOrWhiteSpace(request.NumeroPoliza))
                {
                    query = query.Where(p => p.NumeroPoliza.Contains(request.NumeroPoliza));
                }

                if (request.TipoPolizaId.HasValue)
                {
                    query = query.Where(p => p.TipoPolizaId == request.TipoPolizaId.Value);
                }

                if (request.FechaVencimiento.HasValue)
                {
                    query = query.Where(p => p.FechaVencimiento.Date == request.FechaVencimiento.Value.Date);
                }

                if (!string.IsNullOrWhiteSpace(request.CedulaAsegurado))
                {
                    query = query.Where(p => p.CedulaAsegurado.Contains(request.CedulaAsegurado));
                }

                List<string> cedulasEncontradas = new();

                if (!string.IsNullOrWhiteSpace(request.Nombre) || 
                    !string.IsNullOrWhiteSpace(request.PrimerApellido) || 
                    !string.IsNullOrWhiteSpace(request.SegundoApellido))
                {
                    cedulasEncontradas = await ObtenerCedulasDeClientes(request.Nombre, request.PrimerApellido, request.SegundoApellido);

                    if (cedulasEncontradas.Any())
                    {
                        query = query.Where(p => cedulasEncontradas.Contains(p.CedulaAsegurado));
                    }
                    else
                    {
                        return new ObtenerPolizaResponseModel
                        {
                            Exito = true,
                            Mensaje = "No se encontraron pólizas que coincidan con los criterios de búsqueda.",
                            Data = new List<PolizaEntity>(),
                            Paginacion = new PaginacionModel
                            {
                                PaginaActual = request.Pagina,
                                CantidadRegistros = request.CantidadRegistros,
                                TotalRegistros = 0,
                                CantidadPaginas = 0
                            }
                        };
                    }
                }

                var totalRegistros = await query.CountAsync();

                int cantidadPaginas = (int)Math.Ceiling(totalRegistros / (double)request.CantidadRegistros);

                var polizas = await query
                    .OrderByDescending(p => p.NumeroPoliza)
                    .Skip((request.Pagina - 1) * request.CantidadRegistros)
                    .Take(request.CantidadRegistros)
                    .Include(p => p.TipoPoliza)
                    .Include(p => p.EstadoPoliza)
                    .Include(p => p.TipoCobertura)
                    .ToListAsync();

                return new ObtenerPolizaResponseModel
                {
                    Exito = true,
                    Mensaje = "Pólizas obtenidas correctamente.",
                    Data = polizas,
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
                return new ObtenerPolizaResponseModel
                {
                    Exito = false,
                    Mensaje = $"Error al obtener las pólizas: {ex.Message}",
                    Data = null
                };
            }
        }

        private async Task<bool> VerificarCedulaExiste(string cedulaAsegurado)
        {
            try
            {
                var clienteApiUrl = _configuration["ClienteServiceUrl"] ?? "https://localhost:44383";
                var requestBody = new
                {
                    pagina = 1,
                    cantidadRegistros = 1,
                    cedulaAsegurado = cedulaAsegurado
                };

                var content = new StringContent(
                    JsonSerializer.Serialize(requestBody),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PostAsync($"{clienteApiUrl}/api/cliente/filtros", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var clientesResponse = JsonSerializer.Deserialize<ObtenerClientesResponseDto>(responseContent, options);

                    if (clientesResponse?.Exito == true && clientesResponse.Data != null && clientesResponse.Data.Any())
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task<List<string>> ObtenerCedulasDeClientes(string? nombre, string? primerApellido, string? segundoApellido)
        {
            try
            {
                var clienteApiUrl = _configuration["ClienteServiceUrl"] ?? "https://localhost:44383";
                var requestBody = new
                {
                    pagina = 1,
                    cantidadRegistros = 10000,
                    nombre = nombre,
                    primerApellido = primerApellido,
                    segundoApellido = segundoApellido
                };

                var content = new StringContent(
                    JsonSerializer.Serialize(requestBody),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PostAsync($"{clienteApiUrl}/api/cliente/filtros", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var clientesResponse = JsonSerializer.Deserialize<ObtenerClientesResponseDto>(responseContent, options);

                    if (clientesResponse?.Exito == true && clientesResponse.Data != null)
                    {
                        return clientesResponse.Data.Select(c => c.CedulaAsegurado).ToList();
                    }
                }

                return new List<string>();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public async Task<CrearPolizaResponseModel> CrearPoliza(CrearPolizaRequestModel request)
        {
            try
            {
                // Verificar que la cédula del asegurado existe
                var cedulaExiste = await VerificarCedulaExiste(request.CedulaAsegurado);
                if (!cedulaExiste)
                {
                    return new CrearPolizaResponseModel
                    {
                        Exito = false,
                        Mensaje = $"El cliente con cédula '{request.CedulaAsegurado}' no existe.",
                        Data = null
                    };
                }

                var existente = await _context.PolizaTable
                    .FirstOrDefaultAsync(p => p.NumeroPoliza == request.NumeroPoliza);

                if (existente != null)
                {
                    if (!existente.EstaEliminado)
                    {
                        return new CrearPolizaResponseModel
                        {
                            Exito = false,
                            Mensaje = "Ya existe una póliza con ese número.",
                            Data = null
                        };
                    }

                    existente.TipoPolizaId = request.TipoPolizaId;
                    existente.CedulaAsegurado = request.CedulaAsegurado;
                    existente.MontoAsegurado = request.MontoAsegurado;
                    existente.FechaVencimiento = request.FechaVencimiento;
                    existente.FechaEmision = request.FechaEmision;
                    existente.TipoCoberturaId = request.TipoCoberturaId;
                    existente.EstadoPolizaId = request.EstadoPolizaId;
                    existente.Prima = request.Prima;
                    existente.Periodo = request.Periodo;
                    existente.FechaInclusion = request.FechaInclusion;
                    existente.Aseguradora = request.Aseguradora;
                    existente.EstaEliminado = false;

                    _context.PolizaTable.Update(existente);
                    await _context.SaveChangesAsync();

                    return new CrearPolizaResponseModel
                    {
                        Exito = true,
                        Mensaje = "Póliza restaurada y actualizada correctamente.",
                        Data = existente,
                        Id = existente.Id
                    };
                }

                var poliza = new PolizaEntity
                {
                    NumeroPoliza = request.NumeroPoliza,
                    TipoPolizaId = request.TipoPolizaId,
                    CedulaAsegurado = request.CedulaAsegurado,
                    MontoAsegurado = request.MontoAsegurado,
                    FechaVencimiento = request.FechaVencimiento,
                    FechaEmision = request.FechaEmision,
                    TipoCoberturaId = request.TipoCoberturaId,
                    EstadoPolizaId = request.EstadoPolizaId,
                    Prima = request.Prima,
                    Periodo = request.Periodo,
                    FechaInclusion = request.FechaInclusion,
                    Aseguradora = request.Aseguradora
                };

                _context.PolizaTable.Add(poliza);
                await _context.SaveChangesAsync();

                return new CrearPolizaResponseModel
                {
                    Exito = true,
                    Mensaje = "Póliza creada correctamente.",
                    Data = poliza,
                    Id = poliza.Id
                };
            }
            catch (Exception ex)
            {
                return new CrearPolizaResponseModel
                {
                    Exito = false,
                    Mensaje = $"Error al crear la póliza: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<Poliza.Models.ActualizarPoliza.ActualizarPolizaResponseModel> ActualizarPoliza(Guid id, Poliza.Models.ActualizarPoliza.ActualizarPolizaRequestModel request)
        {
            try
            {
                var poliza = await _context.PolizaTable
                    .FirstOrDefaultAsync(p => p.Id == id && !p.EstaEliminado);

                if (poliza == null)
                {
                    return new Poliza.Models.ActualizarPoliza.ActualizarPolizaResponseModel
                    {
                        Exito = false,
                        Mensaje = "Póliza no encontrada.",
                        Data = null
                    };
                }

                // Verificar que la cédula del asegurado existe (solo si cambió)
                if (poliza.CedulaAsegurado != request.CedulaAsegurado)
                {
                    var cedulaExiste = await VerificarCedulaExiste(request.CedulaAsegurado);
                    if (!cedulaExiste)
                    {
                        return new Poliza.Models.ActualizarPoliza.ActualizarPolizaResponseModel
                        {
                            Exito = false,
                            Mensaje = $"El cliente con cédula '{request.CedulaAsegurado}' no existe.",
                            Data = null
                        };
                    }
                }

                poliza.TipoPolizaId = request.TipoPolizaId;
                poliza.CedulaAsegurado = request.CedulaAsegurado;
                poliza.MontoAsegurado = request.MontoAsegurado;
                poliza.FechaVencimiento = request.FechaVencimiento;
                poliza.FechaEmision = request.FechaEmision;
                poliza.TipoCoberturaId = request.TipoCoberturaId;
                poliza.EstadoPolizaId = request.EstadoPolizaId;
                poliza.Prima = request.Prima;
                poliza.Periodo = request.Periodo;
                poliza.FechaInclusion = request.FechaInclusion;
                poliza.Aseguradora = request.Aseguradora;

                await _context.SaveChangesAsync();

                return new Poliza.Models.ActualizarPoliza.ActualizarPolizaResponseModel
                {
                    Exito = true,
                    Mensaje = "Póliza actualizada correctamente.",
                    Data = poliza,
                    Id = poliza.Id
                };
            }
            catch (Exception ex)
            {
                return new Poliza.Models.ActualizarPoliza.ActualizarPolizaResponseModel
                {
                    Exito = false,
                    Mensaje = $"Error al actualizar la póliza: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<Comun.Models.ResponseModel<bool>> EliminarPoliza(Guid id)
        {
            try
            {
                var poliza = await _context.PolizaTable
                    .FirstOrDefaultAsync(p => p.Id == id && !p.EstaEliminado);

                if (poliza == null)
                {
                    return new Comun.Models.ResponseModel<bool>
                    {
                        Exito = false,
                        Mensaje = "Póliza no encontrada.",
                        Data = false
                    };
                }

                poliza.EstaEliminado = true;
                _context.PolizaTable.Update(poliza);
                await _context.SaveChangesAsync();

                return new Comun.Models.ResponseModel<bool>
                {
                    Exito = true,
                    Mensaje = "Póliza eliminada (soft-delete) correctamente.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new Comun.Models.ResponseModel<bool>
                {
                    Exito = false,
                    Mensaje = $"Error al eliminar la póliza: {ex.Message}",
                    Data = false
                };
            }
        }
    }
}