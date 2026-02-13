namespace Comun.Models
{
    public class PaginacionModel
    {
        public int PaginaActual { get; set; }
        public int CantidadRegistros { get; set; }
        public int TotalRegistros { get; set; }
        public int CantidadPaginas { get; set; }
    }
}
