namespace Comun.Models
{
    public class ResponseModel<T>
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errores { get; set; }
    }
}
