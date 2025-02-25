using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigracionRelacionSolicitudUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Estados_Solicitudes_so_es_id",
                table: "Solicitudes");

            migrationBuilder.AlterColumn<int>(
                name: "so_es_id",
                table: "Solicitudes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "so_us_id",
                table: "Solicitudes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_so_us_id",
                table: "Solicitudes",
                column: "so_us_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_Estados_Solicitudes_so_es_id",
                table: "Solicitudes",
                column: "so_es_id",
                principalTable: "Estados_Solicitudes",
                principalColumn: "es_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_Usuarios_so_us_id",
                table: "Solicitudes",
                column: "so_us_id",
                principalTable: "Usuarios",
                principalColumn: "us_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Estados_Solicitudes_so_es_id",
                table: "Solicitudes");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Usuarios_so_us_id",
                table: "Solicitudes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitudes_so_us_id",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "so_us_id",
                table: "Solicitudes");

            migrationBuilder.AlterColumn<int>(
                name: "so_es_id",
                table: "Solicitudes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_Estados_Solicitudes_so_es_id",
                table: "Solicitudes",
                column: "so_es_id",
                principalTable: "Estados_Solicitudes",
                principalColumn: "es_id");
        }
    }
}
