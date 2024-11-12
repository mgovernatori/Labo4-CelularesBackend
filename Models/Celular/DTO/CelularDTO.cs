using CelularesAPI.Models.Color;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CelularesAPI.Models.Celular.DTO
{
    public class CelularDTO
    {
        public int Id { get; set; }
        public Marca.Marca Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public DateOnly FechaLanzamiento { get; set; }
        public Sistema.Sistema Sistema { get; set; } = null!;
        public string Procesador { get; set; } = null!;
        public string Pantalla { get; set; } = null!;
        public string Camara { get; set; } = null!;
        public string Almacenamiento { get; set; } = null!;
        public string RAM { get; set; } = null!;
        public string Bateria { get; set; } = null!;
        public List<ColorImagen> Colores { get; set; } = null!;
        public string Precio { get; set; } = null!;
    }

    public class ColorImagen
    {
        public string NombreColor { get; set; } = null!;
        public string UrlImagen { get; set; } = null!;
    }
}
