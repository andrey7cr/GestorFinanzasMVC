using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorFinanzasMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioIdToIngresosYGastos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Ingresos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Gastos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_UsuarioId",
                table: "Ingresos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_UsuarioId",
                table: "Gastos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Usuarios_UsuarioId",
                table: "Gastos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingresos_Usuarios_UsuarioId",
                table: "Ingresos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Usuarios_UsuarioId",
                table: "Gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingresos_Usuarios_UsuarioId",
                table: "Ingresos");

            migrationBuilder.DropIndex(
                name: "IX_Ingresos_UsuarioId",
                table: "Ingresos");

            migrationBuilder.DropIndex(
                name: "IX_Gastos_UsuarioId",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Ingresos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Gastos");
        }
    }
}
