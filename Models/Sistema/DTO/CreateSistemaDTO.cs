using System.ComponentModel.DataAnnotations;

namespace CelularesAPI.Models.Sistema.DTO
{
    public class CreateSistemaDTO
    {
        [Required]
        [MaxLength(10)]
        public string Nombre { get; set; } = null!;

        [Required]
        public int Version { get; set; }
    }
}
