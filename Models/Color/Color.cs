using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CelularesAPI.Models.Color
{
    public class Color
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Nombre { get; set; } = null!;


    }

    public class ColoresCelular
    {
        public int IdCelular { get; set; }
        public int IdColor { get; set; }
        public string UrlImagen { get; set; } = null!;
    }
}
