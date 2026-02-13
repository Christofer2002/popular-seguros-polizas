using Microsoft.EntityFrameworkCore;
using Poliza.Data;
using Poliza.Entities;
using Poliza.Interfaces;
using Poliza.Models.CrearPoliza;
using Poliza.Models.ObtenerPolizas;
using Comun.Models;

namespace Poliza.Services
{
    public class PolizaService : IPolizaService
    {
        private readonly PolizaDbContext _context;

        public PolizaService(PolizaDbContext context)
        {
            _context = context;
        }

        public async Task<ObtenerPolizaResponseModel> ObtenerPolizas(PaginacionRequestModel request)
        {
            try
            {
                var totalRegistros = await _context.PolizaTable
                    .Where(p => !p.EstaEliminado)
                    .CountAsync();

                int cantidadPaginas = (int)Math.Ceiling(totalRegistros / (double)request.CantidadRegistros);

                var polizas = await _context.PolizaTable
                    .Where(p => !p.EstaEliminado)
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

        public async Task<CrearPolizaResponseModel> CrearPoliza(CrearPolizaRequestModel request)
        {
            try
            {
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