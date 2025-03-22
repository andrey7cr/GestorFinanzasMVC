using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorFinanzasMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddIngresosGastosCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Nombre);
                });

            migrationBuilder.CreateTable(
                name: "Gastos",
                columns: table => new
                {
                    GastoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    CategoriaNombre = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gastos", x => x.GastoId);
                    table.ForeignKey(
                        name: "FK_Gastos_Categorias_CategoriaNombre",
                        column: x => x.CategoriaNombre,
                        principalTable: "Categorias",
                        principalColumn: "Nombre");
                });

            migrationBuilder.CreateTable(
                name: "Ingresos",
                columns: table => new
                {
                    IngresoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    CategoriaNombre = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingresos", x => x.IngresoId);
                    table.ForeignKey(
                        name: "FK_Ingresos_Categorias_CategoriaNombre",
                        column: x => x.CategoriaNombre,
                        principalTable: "Categorias",
                        principalColumn: "Nombre");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_CategoriaNombre",
                table: "Gastos",
                column: "CategoriaNombre");

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_CategoriaNombre",
                table: "Ingresos",
                column: "CategoriaNombre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gastos");

            migrationBuilder.DropTable(
                name: "Ingresos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
