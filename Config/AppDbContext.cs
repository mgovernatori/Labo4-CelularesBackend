using CelularesAPI.Models.Celular;
using CelularesAPI.Models.Sistema;
using CelularesAPI.Models.Usuario;
using CelularesAPI.Models.Marca;
using CelularesAPI.Models.Rol;
using Microsoft.EntityFrameworkCore;
using CelularesAPI.Models.Usuario.DTO;
using CelularesAPI.Models.Color;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CelularesAPI.Config
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Celular> Celulares { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Sistema> Sistemas { get; set; }
        public DbSet<Color> Colores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Username).IsUnique();

            var dateConverter = new ValueConverter<DateOnly, DateTime>(
                v => v.ToDateTime(new TimeOnly(0, 0)),
                v => DateOnly.FromDateTime(v)
            );

            modelBuilder.Entity<Celular>().Property(e => e.FechaLanzamiento).HasConversion(dateConverter);

            modelBuilder.Entity<Celular>().HasData(
                new Celular
                {
                    Id = 1,
                    IdMarca = 1,
                    Modelo = "Galaxy S24",
                    FechaLanzamiento = new DateOnly(2024, 1, 24),
                    IdSistema = 1,
                    Procesador = "Exynos 2400",
                    Pantalla = "6.2 pulgadas",
                    Camara = "Triple, 50 megapíxeles",
                    Almacenamiento = "256GB",
                    RAM = "8GB",
                    Bateria = "4000 mAh",
                    Precio = "$1.200.000",
                },
                new Celular
                {
                    Id = 2,
                    IdMarca = 1,
                    Modelo = "Galaxy S24+",
                    FechaLanzamiento = new DateOnly(2024, 1, 24),
                    IdSistema = 1,
                    Procesador = "Exynos 1480",
                    Pantalla = "6.7 pulgadas",
                    Camara = "Triple, 50 megapíxeles",
                    Almacenamiento = "256GB",
                    RAM = "12GB",
                    Bateria = "4900 mAh",
                    Precio = "$1.500.000",
                }  
            );

            modelBuilder.Entity<Marca>().HasData(
                new Marca { Id = 1, Nombre = "Samsung" },
                new Marca { Id = 2, Nombre = "Apple" }
            );

            modelBuilder.Entity<Sistema>().HasData(
                new Sistema { Id = 1, Nombre = "Android", Version = 14 },
                new Sistema { Id = 2, Nombre = "iOS", Version = 18 }
            );

            modelBuilder.Entity<Color>().HasData(
                new Color { Id = 1, Nombre = "Onyx Black" },
                new Color { Id = 2, Nombre = "Marble Gray" },
                new Color { Id = 3, Nombre = "Cobalt Violet" },
                new Color { Id = 4, Nombre = "Amber Yellow" }
            );

            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Nombre = "Administrador" },
                new Rol { Id = 2, Nombre = "Moderador" },
                new Rol { Id = 3, Nombre = "Usuario" }
            );

            modelBuilder.Entity<ColoresCelular>().HasData(                    
                new ColoresCelular { IdCelular = 1, IdColor = 1, UrlImagen = "https://samsungar.vtexassets.com/arquivos/ids/193556-800-auto?width=800&height=auto&aspect=true" },
                new ColoresCelular { IdCelular = 1, IdColor = 2, UrlImagen = "https://samsungar.vtexassets.com/arquivos/ids/193565-800-auto?width=800&height=auto&aspect=true" },
                new ColoresCelular { IdCelular = 1, IdColor = 3, UrlImagen = "https://samsungar.vtexassets.com/arquivos/ids/193683-800-auto?width=800&height=auto&aspect=true" },
                new ColoresCelular { IdCelular = 1, IdColor = 4, UrlImagen = "https://samsungar.vtexassets.com/arquivos/ids/195008-800-auto?width=800&height=auto&aspect=true" },
                new ColoresCelular { IdCelular = 2, IdColor = 1, UrlImagen = "https://samsungar.vtexassets.com/arquivos/ids/193692-800-auto?width=800&height=auto&aspect=true" },
                new ColoresCelular { IdCelular = 2, IdColor = 2, UrlImagen = "https://samsungar.vtexassets.com/arquivos/ids/193701-800-auto?width=800&height=auto&aspect=true" },
                new ColoresCelular { IdCelular = 2, IdColor = 3, UrlImagen = "https://samsungar.vtexassets.com/arquivos/ids/193710-800-auto?width=800&height=auto&aspect=true" },
                new ColoresCelular { IdCelular = 2, IdColor = 4, UrlImagen = "https://samsungar.vtexassets.com/arquivos/ids/193719-800-auto?width=800&height=auto&aspect=true" }
            );

            modelBuilder.Entity<Usuario>().HasMany(u => u.Roles).WithMany().UsingEntity<RolUsuario>(
                u => u.HasOne<Rol>().WithMany().HasForeignKey(e => e.IdRol),
                r => r.HasOne<Usuario>().WithMany().HasForeignKey(e => e.IdUsuario)
            );

            modelBuilder.Entity<Celular>().HasMany(c => c.Colores).WithMany().UsingEntity<ColoresCelular>(
                ce => ce.HasOne<Color>().WithMany().HasForeignKey(e => e.IdColor),
                co => co.HasOne<Celular>().WithMany().HasForeignKey(e => e.IdCelular)
            );
        }
    }
}
