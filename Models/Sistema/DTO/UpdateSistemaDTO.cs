using System.ComponentModel.DataAnnotations;

namespace CelularesAPI.Models.Sistema.DTO
{
    public class UpdateSistemaDTO
    {

        [MaxLength(10)]
        public string? Nombre { get; set; } = null!;

        public int? Version { get; set; }
    }
}
