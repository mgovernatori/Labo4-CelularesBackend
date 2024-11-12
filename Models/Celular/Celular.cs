using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CelularesAPI.Models.Celular
{
    public class Celular
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int IdMarca { get; set; }

        [ForeignKey("IdMarca")]
        public Marca.Marca Marca { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Modelo { get; set; } = null!;

        [Required]
        public DateOnly FechaLanzamiento { get; set; }

        [Required]
        public int IdSistema { get; set; }

        [ForeignKey("IdSistema")]
        public Sistema.Sistema Sistema { get; set; } = null!;

        [Required]
        [MaxLength(40)]
        public string Procesador { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Pantalla { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string Camara { get; set; } = null!;

        [Required]
        [MaxLength(40)]
        public string Almacenamiento { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string RAM { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Bateria { get; set; } = null!;

        public List<Color.Color> Colores { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Precio { get; set; } = null!;

    }
}
