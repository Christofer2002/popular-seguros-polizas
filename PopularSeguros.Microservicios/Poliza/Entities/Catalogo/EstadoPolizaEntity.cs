using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poliza.Entities.Catalogo
{
    [Table("EstadoPolizaTable", Schema = "CatalogoSchema")]
    public class EstadoPolizaEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;
    }
}