using BCrypt.Net;

namespace Autenticacion.Utilities
{
    public class PasswordHashGenerator
    {
        public static string GenerarHash(string contraseña)
        {
            return BCrypt.Net.BCrypt.HashPassword(contraseña);
        }

        public static bool VerificarContraseña(string contraseña, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(contraseña, hash);
        }
    }
}
