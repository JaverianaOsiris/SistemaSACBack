using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigracionCreacionLogin2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Login",
                newName: "lo_password");

            migrationBuilder.RenameColumn(
                name: "FechaIngreso",
                table: "Login",
                newName: "lo_fechaIngreso");

            migrationBuilder.RenameColumn(
                name: "Bloqueo",
                table: "Login",
                newName: "lo_bloqueo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lo_password",
                table: "Login",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "lo_fechaIngreso",
                table: "Login",
                newName: "FechaIngreso");

            migrationBuilder.RenameColumn(
                name: "lo_bloqueo",
                table: "Login",
                newName: "Bloqueo");
        }
    }
}
