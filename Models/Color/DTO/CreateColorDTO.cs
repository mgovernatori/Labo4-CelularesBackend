using System.ComponentModel.DataAnnotations;

namespace CelularesAPI.Models.Color.DTO
{
    public class CreateColorDTO
    {
        [Required]
        [MaxLength(40)]
        public string Nombre { get; set; } = null!;
    }
}
