using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTanlaUsariosYSolicitudes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Colaboradores_Colaboradorescol_id",
                table: "Solicitudes");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Tipos_Usuarios_Tipos_Usuariostu_id",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Tipos_Usuariostu_id",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Solicitudes_Colaboradorescol_id",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "Tipos_Usuariostu_id",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Colaboradorescol_id",
                table: "Solicitudes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tipos_Usuariostu_id",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Colaboradorescol_id",
                table: "Solicitudes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Tipos_Usuariostu_id",
                table: "Usuarios",
                column: "Tipos_Usuariostu_id");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_Colaboradorescol_id",
                table: "Solicitudes",
                column: "Colaboradorescol_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_Colaboradores_Colaboradorescol_id",
                table: "Solicitudes",
                column: "Colaboradorescol_id",
                principalTable: "Colaboradores",
                principalColumn: "col_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Tipos_Usuarios_Tipos_Usuariostu_id",
                table: "Usuarios",
                column: "Tipos_Usuariostu_id",
                principalTable: "Tipos_Usuarios",
                principalColumn: "tu_id");
        }
    }
}
