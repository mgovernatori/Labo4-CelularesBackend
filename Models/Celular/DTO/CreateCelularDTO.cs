using System.ComponentModel.DataAnnotations;

namespace CelularesAPI.Models.Celular.DTO
{
    public class CreateCelularDTO
    {
        [Required]
        public int IdMarca { get; set; }

        [Required]
        [MaxLength(50)]
        public string Modelo { get; set; } = null!;

        [Required]
        public DateOnly FechaLanzamiento { get; set; }

        [Required]
        public int IdSistema { get; set; }

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

        public List<ColorImagen> Colores { get; set; } = null!;


        [Required]
        [MaxLength(20)]
        public string Precio { get; set; } = null!;

    }


}
