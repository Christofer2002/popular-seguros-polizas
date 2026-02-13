namespace Poliza.Models.Cliente
{
    public class ClienteDto
    {
        public string CedulaAsegurado { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string PrimerApellido { get; set; } = string.Empty;
        public string? SegundoApellido { get; set; }
        public string TipoPersona { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
    }

    public class ObtenerClientesResponseDto
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public List<ClienteDto>? Data { get; set; }
        public object? Paginacion { get; set; }
    }
}
