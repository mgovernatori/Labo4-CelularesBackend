using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CelularesAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sistemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sistemas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Celulares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMarca = table.Column<int>(type: "int", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaLanzamiento = table.Column<DateTime>(type: "date", nullable: false),
                    IdSistema = table.Column<int>(type: "int", nullable: false),
                    Procesador = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Pantalla = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Camara = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Almacenamiento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RAM = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Bateria = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Precio = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Celulares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Celulares_Marcas_IdMarca",
                        column: x => x.IdMarca,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Celulares_Sistemas_IdSistema",
                        column: x => x.IdSistema,
                        principalTable: "Sistemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolUsuario",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolUsuario", x => new { x.IdRol, x.IdUsuario });
                    table.ForeignKey(
                        name: "FK_RolUsuario_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolUsuario_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColoresCelular",
                columns: table => new
                {
                    IdCelular = table.Column<int>(type: "int", nullable: false),
                    IdColor = table.Column<int>(type: "int", nullable: false),
                    UrlImagen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColoresCelular", x => new { x.IdCelular, x.IdColor });
                    table.ForeignKey(
                        name: "FK_ColoresCelular_Celulares_IdCelular",
                        column: x => x.IdCelular,
                        principalTable: "Celulares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColoresCelular_Colores_IdColor",
                        column: x => x.IdColor,
                        principalTable: "Colores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Colores",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Onyx Black" },
                    { 2, "Marble Gray" },
                    { 3, "Cobalt Violet" },
                    { 4, "Amber Yellow" }
                });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Samsung" },
                    { 2, "Apple" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Moderador" },
                    { 3, "Usuario" }
                });

            migrationBuilder.InsertData(
                table: "Sistemas",
                columns: new[] { "Id", "Nombre", "Version" },
                values: new object[,]
                {
                    { 1, "Android", 14 },
                    { 2, "iOS", 18 }
                });

            migrationBuilder.InsertData(
                table: "Celulares",
                columns: new[] { "Id", "Almacenamiento", "Bateria", "Camara", "FechaLanzamiento", "IdMarca", "IdSistema", "Modelo", "Pantalla", "Precio", "Procesador", "RAM" },
                values: new object[,]
                {
                    { 1, "256GB", "4000 mAh", "Triple, 50 megapíxeles", new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Galaxy S24", "6.2 pulgadas", "$1.200.000", "Exynos 2400", "8GB" },
                    { 2, "256GB", "4900 mAh", "Triple, 50 megapíxeles", new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Galaxy S24+", "6.7 pulgadas", "$1.500.000", "Exynos 1480", "12GB" }
                });

            migrationBuilder.InsertData(
                table: "ColoresCelular",
                columns: new[] { "IdCelular", "IdColor", "UrlImagen" },
                values: new object[,]
                {
                    { 1, 1, "https://samsungar.vtexassets.com/arquivos/ids/193556-800-auto?width=800&height=auto&aspect=true" },
                    { 1, 2, "https://samsungar.vtexassets.com/arquivos/ids/193565-800-auto?width=800&height=auto&aspect=true" },
                    { 1, 3, "https://samsungar.vtexassets.com/arquivos/ids/193683-800-auto?width=800&height=auto&aspect=true" },
                    { 1, 4, "https://samsungar.vtexassets.com/arquivos/ids/195008-800-auto?width=800&height=auto&aspect=true" },
                    { 2, 1, "https://samsungar.vtexassets.com/arquivos/ids/193692-800-auto?width=800&height=auto&aspect=true" },
                    { 2, 2, "https://samsungar.vtexassets.com/arquivos/ids/193701-800-auto?width=800&height=auto&aspect=true" },
                    { 2, 3, "https://samsungar.vtexassets.com/arquivos/ids/193710-800-auto?width=800&height=auto&aspect=true" },
                    { 2, 4, "https://samsungar.vtexassets.com/arquivos/ids/193719-800-auto?width=800&height=auto&aspect=true" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Celulares_IdMarca",
                table: "Celulares",
                column: "IdMarca");

            migrationBuilder.CreateIndex(
                name: "IX_Celulares_IdSistema",
                table: "Celulares",
                column: "IdSistema");

            migrationBuilder.CreateIndex(
                name: "IX_ColoresCelular_IdColor",
                table: "ColoresCelular",
                column: "IdColor");

            migrationBuilder.CreateIndex(
                name: "IX_RolUsuario_IdUsuario",
                table: "RolUsuario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Username",
                table: "Usuarios",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColoresCelular");

            migrationBuilder.DropTable(
                name: "RolUsuario");

            migrationBuilder.DropTable(
                name: "Celulares");

            migrationBuilder.DropTable(
                name: "Colores");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Sistemas");
        }
    }
}
