using Comun.Models;

namespace Comun.Models
{
    public class PaginacionResponseModel<T> : ResponseModel<T>
    {
        public PaginacionModel? Paginacion { get; set; }
    }
}
