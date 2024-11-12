using CelularesAPI.Models.Rol;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CelularesAPI.Models.Usuario
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set;} = null!;

        [Required]
        [MinLength(8)]
        public string Contraseña { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Username { get; set; } = null!;

        public List<Rol.Rol> Roles { get; set; } = null!;




    }
}
