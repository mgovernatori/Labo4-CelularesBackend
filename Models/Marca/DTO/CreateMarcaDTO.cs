using System.ComponentModel.DataAnnotations;

namespace CelularesAPI.Models.Marca.DTO
{
    public class CreateMarcaDTO
    {
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; } = null!;
    }
}
