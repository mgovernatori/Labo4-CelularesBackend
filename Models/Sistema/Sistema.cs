using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CelularesAPI.Models.Sistema
{
    public class Sistema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Nombre { get; set; } = null!;

        [Required]
        public int Version { get; set; }
    }
}
