using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigracionSolicitudesYRelaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    so_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    so_numero_solicitud = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    so_ts_id = table.Column<int>(type: "int", nullable: false),
                    so_descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    so_fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.so_id);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Tipos_Solicitudes_so_ts_id",
                        column: x => x.so_ts_id,
                        principalTable: "Tipos_Solicitudes",
                        principalColumn: "ts_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_so_ts_id",
                table: "Solicitudes",
                column: "so_ts_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Solicitudes");
        }
    }
}
