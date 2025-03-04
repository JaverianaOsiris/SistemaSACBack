using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTanlaUsariosYSolicitudes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "so_id_colaborador_modificacion",
                table: "Solicitudes",
                newName: "so_col_id_colaborador_modificacion");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_us_tu_id",
                table: "Usuarios",
                column: "us_tu_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Tipos_Usuarios_us_tu_id",
                table: "Usuarios",
                column: "us_tu_id",
                principalTable: "Tipos_Usuarios",
                principalColumn: "tu_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Tipos_Usuarios_us_tu_id",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_us_tu_id",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "so_col_id_colaborador_modificacion",
                table: "Solicitudes",
                newName: "so_id_colaborador_modificacion");
        }
    }
}
