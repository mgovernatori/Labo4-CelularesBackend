using System.ComponentModel.DataAnnotations;

namespace CelularesAPI.Models.Auth
{
    public class Login
    {
        public string? Username { get; set; }

        [EmailAddress]
        public string? Email { get; set; }


        [Required]
        public string Contraseña { get; set; } = null!;


    }
}
