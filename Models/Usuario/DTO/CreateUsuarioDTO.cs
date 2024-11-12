using System.ComponentModel.DataAnnotations;

namespace CelularesAPI.Models.Usuario.DTO
{
    public class CreateUsuarioDTO
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(8)]
        public string Contraseña { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Username { get; set; } = null!;

    }

}
