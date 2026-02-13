using System.ComponentModel.DataAnnotations;
using Comun.Models;

namespace Cliente.Models.ObtenerClientes
{
    public class ObtenerClientesRequestModel : PaginacionRequestModel
    {
        [StringLength(20)]
        [RegularExpression(@"^[0-9\-]+$", ErrorMessage = "La cédula solo puede contener números y guiones.")]
        public string? CedulaAsegurado { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-ZÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El nombre solo puede contener letras.")]
        public string? Nombre { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-ZÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El primer apellido solo puede contener letras.")]
        public string? PrimerApellido { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-ZÁÉÍÓÚáéíóúÑñ\s]*$", ErrorMessage = "El segundo apellido solo puede contener letras.")]
        public string? SegundoApellido { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^[a-zA-ZÁÉÍÓÚáéíóúÑñ\s]*$", ErrorMessage = "El tipo de persona solo puede contener letras.")]
        public string? TipoPersona { get; set; }
    }
}
