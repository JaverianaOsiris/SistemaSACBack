using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AsociacionLiderAColaborador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "col_col_id_lider",
                table: "Colaboradores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_col_col_id_lider",
                table: "Colaboradores",
                column: "col_col_id_lider");

            migrationBuilder.AddForeignKey(
                name: "FK_Colaboradores_Colaboradores_col_col_id_lider",
                table: "Colaboradores",
                column: "col_col_id_lider",
                principalTable: "Colaboradores",
                principalColumn: "col_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colaboradores_Colaboradores_col_col_id_lider",
                table: "Colaboradores");

            migrationBuilder.DropIndex(
                name: "IX_Colaboradores_col_col_id_lider",
                table: "Colaboradores");

            migrationBuilder.DropColumn(
                name: "col_col_id_lider",
                table: "Colaboradores");
        }
    }
}
