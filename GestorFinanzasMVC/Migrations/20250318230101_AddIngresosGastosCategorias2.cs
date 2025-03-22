using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorFinanzasMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddIngresosGastosCategorias2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Categorias_CategoriaNombre",
                table: "Gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingresos_Categorias_CategoriaNombre",
                table: "Ingresos");

            migrationBuilder.DropIndex(
                name: "IX_Ingresos_CategoriaNombre",
                table: "Ingresos");

            migrationBuilder.DropIndex(
                name: "IX_Gastos_CategoriaNombre",
                table: "Gastos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "CategoriaNombre",
                table: "Ingresos");

            migrationBuilder.DropColumn(
                name: "CategoriaNombre",
                table: "Gastos");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Categorias",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_CategoriaId",
                table: "Ingresos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_CategoriaId",
                table: "Gastos",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Categorias_CategoriaId",
                table: "Gastos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingresos_Categorias_CategoriaId",
                table: "Ingresos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Categorias_CategoriaId",
                table: "Gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingresos_Categorias_CategoriaId",
                table: "Ingresos");

            migrationBuilder.DropIndex(
                name: "IX_Ingresos_CategoriaId",
                table: "Ingresos");

            migrationBuilder.DropIndex(
                name: "IX_Gastos_CategoriaId",
                table: "Gastos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Categorias");

            migrationBuilder.AddColumn<string>(
                name: "CategoriaNombre",
                table: "Ingresos",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoriaNombre",
                table: "Gastos",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_CategoriaNombre",
                table: "Ingresos",
                column: "CategoriaNombre");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_CategoriaNombre",
                table: "Gastos",
                column: "CategoriaNombre");

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Categorias_CategoriaNombre",
                table: "Gastos",
                column: "CategoriaNombre",
                principalTable: "Categorias",
                principalColumn: "Nombre");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingresos_Categorias_CategoriaNombre",
                table: "Ingresos",
                column: "CategoriaNombre",
                principalTable: "Categorias",
                principalColumn: "Nombre");
        }
    }
}
