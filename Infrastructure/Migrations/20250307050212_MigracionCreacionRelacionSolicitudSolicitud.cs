using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigracionCreacionRelacionSolicitudSolicitud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "so_so_id",
                table: "Solicitudes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_so_so_id",
                table: "Solicitudes",
                column: "so_so_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_Solicitudes_so_so_id",
                table: "Solicitudes",
                column: "so_so_id",
                principalTable: "Solicitudes",
                principalColumn: "so_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Solicitudes_so_so_id",
                table: "Solicitudes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitudes_so_so_id",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "so_so_id",
                table: "Solicitudes");
        }
    }
}
