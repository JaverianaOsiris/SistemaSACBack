using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreacionHistoricosYColaboradores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tipos_Usuariostu_id",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "us_tu_id",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Colaboradorescol_id",
                table: "Solicitudes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "so_col_id",
                table: "Solicitudes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "so_fecha_modificacion",
                table: "Solicitudes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "so_id_colaborador_modificacion",
                table: "Solicitudes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "so_observaciones",
                table: "Solicitudes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "so_respuesta",
                table: "Solicitudes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tipos_Colaboradores",
                columns: table => new
                {
                    tc_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tc_cargo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tc_descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Colaboradores", x => x.tc_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tipos_Usuarios",
                columns: table => new
                {
                    tu_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tu_tipo_usuario = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Usuarios", x => x.tu_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Colaboradores",
                columns: table => new
                {
                    col_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    col_nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    col_apellido = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    col_identificacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    col_telefono = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    col_email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    col_tc_id = table.Column<int>(type: "int", nullable: false),
                    col_tu_id = table.Column<int>(type: "int", nullable: false),
                    col_activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaboradores", x => x.col_id);
                    table.ForeignKey(
                        name: "FK_Colaboradores_Tipos_Colaboradores_col_tc_id",
                        column: x => x.col_tc_id,
                        principalTable: "Tipos_Colaboradores",
                        principalColumn: "tc_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Colaboradores_Tipos_Usuarios_col_tu_id",
                        column: x => x.col_tu_id,
                        principalTable: "Tipos_Usuarios",
                        principalColumn: "tu_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cantidad_Solicitudes",
                columns: table => new
                {
                    cs_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cs_col_id = table.Column<int>(type: "int", nullable: false),
                    cs_ts_id = table.Column<int>(type: "int", nullable: false),
                    cs_es_id = table.Column<int>(type: "int", nullable: false),
                    cs_cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cantidad_Solicitudes", x => x.cs_id);
                    table.ForeignKey(
                        name: "FK_Cantidad_Solicitudes_Colaboradores_cs_col_id",
                        column: x => x.cs_col_id,
                        principalTable: "Colaboradores",
                        principalColumn: "col_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cantidad_Solicitudes_Estados_Solicitudes_cs_es_id",
                        column: x => x.cs_es_id,
                        principalTable: "Estados_Solicitudes",
                        principalColumn: "es_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cantidad_Solicitudes_Tipos_Solicitudes_cs_ts_id",
                        column: x => x.cs_ts_id,
                        principalTable: "Tipos_Solicitudes",
                        principalColumn: "ts_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Historicos_Solicitudes",
                columns: table => new
                {
                    hs_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hs_so_id = table.Column<int>(type: "int", nullable: false),
                    hs_es_id = table.Column<int>(type: "int", nullable: false),
                    hs_col_id = table.Column<int>(type: "int", nullable: false),
                    hs_detalle = table.Column<int>(type: "int", nullable: false),
                    hs_fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historicos_Solicitudes", x => x.hs_id);
                    table.ForeignKey(
                        name: "FK_Historicos_Solicitudes_Colaboradores_hs_col_id",
                        column: x => x.hs_col_id,
                        principalTable: "Colaboradores",
                        principalColumn: "col_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historicos_Solicitudes_Estados_Solicitudes_hs_es_id",
                        column: x => x.hs_es_id,
                        principalTable: "Estados_Solicitudes",
                        principalColumn: "es_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historicos_Solicitudes_Solicitudes_hs_so_id",
                        column: x => x.hs_so_id,
                        principalTable: "Solicitudes",
                        principalColumn: "so_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Tipos_Usuariostu_id",
                table: "Usuarios",
                column: "Tipos_Usuariostu_id");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_Colaboradorescol_id",
                table: "Solicitudes",
                column: "Colaboradorescol_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cantidad_Solicitudes_cs_col_id",
                table: "Cantidad_Solicitudes",
                column: "cs_col_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cantidad_Solicitudes_cs_es_id",
                table: "Cantidad_Solicitudes",
                column: "cs_es_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cantidad_Solicitudes_cs_ts_id",
                table: "Cantidad_Solicitudes",
                column: "cs_ts_id");

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_col_tc_id",
                table: "Colaboradores",
                column: "col_tc_id");

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_col_tu_id",
                table: "Colaboradores",
                column: "col_tu_id");

            migrationBuilder.CreateIndex(
                name: "IX_Historicos_Solicitudes_hs_col_id",
                table: "Historicos_Solicitudes",
                column: "hs_col_id");

            migrationBuilder.CreateIndex(
                name: "IX_Historicos_Solicitudes_hs_es_id",
                table: "Historicos_Solicitudes",
                column: "hs_es_id");

            migrationBuilder.CreateIndex(
                name: "IX_Historicos_Solicitudes_hs_so_id",
                table: "Historicos_Solicitudes",
                column: "hs_so_id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Colaboradores_Colaboradorescol_id",
                table: "Solicitudes");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Tipos_Usuarios_Tipos_Usuariostu_id",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Cantidad_Solicitudes");

            migrationBuilder.DropTable(
                name: "Historicos_Solicitudes");

            migrationBuilder.DropTable(
                name: "Colaboradores");

            migrationBuilder.DropTable(
                name: "Tipos_Colaboradores");

            migrationBuilder.DropTable(
                name: "Tipos_Usuarios");

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
                name: "us_tu_id",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Colaboradorescol_id",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "so_col_id",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "so_fecha_modificacion",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "so_id_colaborador_modificacion",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "so_observaciones",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "so_respuesta",
                table: "Solicitudes");
        }
    }
}
