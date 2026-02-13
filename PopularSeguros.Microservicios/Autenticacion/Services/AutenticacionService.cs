using Autenticacion.Data;
using Autenticacion.Entities;
using Autenticacion.Interfaces;
using Autenticacion.Models.Login;
using BCrypt.Net;
using Comun.Models;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.Services
{
    public class AutenticacionService : IAutenticacionService
    {
        private readonly AutenticacionDbContext _context;

        public AutenticacionService(AutenticacionDbContext context)
        {
            _context = context;
        }

        public async Task<LoginResponseModel> Login(LoginRequestModel request)
        {
            try
            {
                var usuario = await _context.UsuarioTable
                    .FirstOrDefaultAsync(u => u.NombreUsuario == request.NombreUsuario);

                if (usuario == null)
                {
                    return new LoginResponseModel
                    {
                        Exito = false,
                        Mensaje = "Usuario o contraseña incorrectos.",
                        Data = null
                    };
                }

                if (!usuario.Activo)
                {
                    return new LoginResponseModel
                    {
                        Exito = false,
                        Mensaje = "La cuenta de usuario está inactiva.",
                        Data = null
                    };
                }

                if (!BCrypt.Net.BCrypt.Verify(request.Contraseña, usuario.Contraseña))
                {
                    return new LoginResponseModel
                    {
                        Exito = false,
                        Mensaje = "Usuario o contraseña incorrectos.",
                        Data = null
                    };
                }

                usuario.FechaUltimoLogin = DateTime.UtcNow;

                _context.UsuarioTable.Update(usuario);
                await _context.SaveChangesAsync();

                var usuarioDto = new UsuarioLoginDto
                {
                    Id = usuario.Id,
                    NombreUsuario = usuario.NombreUsuario,
                    Email = usuario.Email,
                    FechaCreacion = usuario.FechaCreacion,
                    FechaUltimoLogin = usuario.FechaUltimoLogin
                };

                return new LoginResponseModel
                {
                    Exito = true,
                    Mensaje = "Login exitoso.",
                    Data = usuarioDto
                };
            }
            catch (Exception ex)
            {
                return new LoginResponseModel
                {
                    Exito = false,
                    Mensaje = $"Error al realizar el login: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ResponseModel<bool>> VerificarDisponibilidadUsuario(string nombreUsuario)
        {
            try
            {
                var usuarioExistente = await _context.UsuarioTable
                    .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

                if (usuarioExistente != null)
                {
                    return new ResponseModel<bool>
                    {
                        Exito = false,
                        Mensaje = "El nombre de usuario ya está en uso.",
                        Data = false
                    };
                }

                return new ResponseModel<bool>
                {
                    Exito = true,
                    Mensaje = "El nombre de usuario está disponible.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool>
                {
                    Exito = false,
                    Mensaje = $"Error al verificar disponibilidad: {ex.Message}",
                    Data = false
                };
            }
        }

        public static string EncriptarContraseña(string contraseña)
        {
            return BCrypt.Net.BCrypt.HashPassword(contraseña);
        }
    }
}
