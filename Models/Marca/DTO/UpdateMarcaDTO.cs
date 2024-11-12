using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CelularesAPI.Models.Marca.DTO
{
    public class UpdateMarcaDTO
    {
        [MaxLength(30)]
        public string? Nombre { get; set; } = null!;
    }
}
