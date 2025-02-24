using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigracionCorreoTelUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "telefono",
                table: "Usuarios",
                newName: "us_telefono");

            migrationBuilder.RenameColumn(
                name: "correo",
                table: "Usuarios",
                newName: "us_correo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "us_telefono",
                table: "Usuarios",
                newName: "telefono");

            migrationBuilder.RenameColumn(
                name: "us_correo",
                table: "Usuarios",
                newName: "correo");
        }
    }
}
