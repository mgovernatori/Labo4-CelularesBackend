using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CelularesAPI.Models.Celular.DTO
{
    public class UpdateCelularDTO
    {

        public int? IdMarca { get; set; }

        [MaxLength(50)]
        public string? Modelo { get; set; } = null!;


        public DateOnly? FechaLanzamiento { get; set; }

        public int? IdSistema { get; set; }

        [MaxLength(40)]
        public string? Procesador { get; set; } = null!;

        [MaxLength(20)]
        public string? Pantalla { get; set; } = null!;

        [MaxLength(30)]
        public string? Camara { get; set; } = null!;

        [MaxLength(40)]
        public string? Almacenamiento { get; set; } = null!;

        [MaxLength(20)]
        public string? RAM { get; set; } = null!;

        [MaxLength(20)]
        public string? Bateria { get; set; } = null!;

        public List<ColorImagen>? Colores { get; set; } = null!;

        [MaxLength(20)]
        public string? Precio { get; set; } = null!;

    }

}
