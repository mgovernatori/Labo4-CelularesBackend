using System.ComponentModel.DataAnnotations;

namespace CelularesAPI.Models.Usuario.DTO
{
    public class UpdateUsuarioDTO
    {
        [MaxLength(50)]
        public string? Nombre { get; set; } = null!;

        [EmailAddress]
        public string? Email { get; set; } = null!;

        [MaxLength(20)]
        public string? Username { get; set; } = null!;

    }
}
