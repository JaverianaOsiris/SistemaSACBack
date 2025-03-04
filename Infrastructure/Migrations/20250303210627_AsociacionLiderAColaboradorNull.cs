using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AsociacionLiderAColaboradorNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colaboradores_Colaboradores_col_col_id_lider",
                table: "Colaboradores");

            migrationBuilder.AlterColumn<int>(
                name: "col_col_id_lider",
                table: "Colaboradores",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Colaboradores_Colaboradores_col_col_id_lider",
                table: "Colaboradores",
                column: "col_col_id_lider",
                principalTable: "Colaboradores",
                principalColumn: "col_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colaboradores_Colaboradores_col_col_id_lider",
                table: "Colaboradores");

            migrationBuilder.AlterColumn<int>(
                name: "col_col_id_lider",
                table: "Colaboradores",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Colaboradores_Colaboradores_col_col_id_lider",
                table: "Colaboradores",
                column: "col_col_id_lider",
                principalTable: "Colaboradores",
                principalColumn: "col_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
