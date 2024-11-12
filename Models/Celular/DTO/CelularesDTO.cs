namespace CelularesAPI.Models.Celular.DTO
{
    public class CelularesDTO
    {
        public int Id { get; set; }
        public Marca.Marca Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public Sistema.Sistema Sistema { get; set; } = null!;
        public string Almacenamiento { get; set; } = null!;
        public string Precio { get; set; } = null!;
    }
}
