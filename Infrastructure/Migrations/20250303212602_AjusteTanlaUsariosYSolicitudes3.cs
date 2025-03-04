using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTanlaUsariosYSolicitudes3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "so_col_id_colaborador_modificacion",
                table: "Solicitudes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "so_col_id",
                table: "Solicitudes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_so_col_id_colaborador_modificacion",
                table: "Solicitudes",
                column: "so_col_id_colaborador_modificacion");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_Colaboradores_so_col_id_colaborador_modificacion",
                table: "Solicitudes",
                column: "so_col_id_colaborador_modificacion",
                principalTable: "Colaboradores",
                principalColumn: "col_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Colaboradores_so_col_id_colaborador_modificacion",
                table: "Solicitudes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitudes_so_col_id_colaborador_modificacion",
                table: "Solicitudes");

            migrationBuilder.AlterColumn<int>(
                name: "so_col_id_colaborador_modificacion",
                table: "Solicitudes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "so_col_id",
                table: "Solicitudes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
