using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SIGO.Consultorias.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "empresas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    razao_social = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    nome_fantasia = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    contrato_ativo = table.Column<bool>(type: "boolean", maxLength: 18, nullable: false),
                    data_inclusao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    data_alteracao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "analises",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_empresa = table.Column<int>(type: "integer", nullable: false),
                    titulo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    resumo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    id_usuario_inclusao = table.Column<int>(type: "integer", nullable: false),
                    usuario_inclusao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    id_usuario_alteracao = table.Column<int>(type: "integer", nullable: true),
                    usuario_alteracao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    data_publicacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    data_inclusao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    data_alteracao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_analises", x => x.id);
                    table.ForeignKey(
                        name: "FK_analises_empresas_id_empresa",
                        column: x => x.id_empresa,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "empresas_usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_usuario = table.Column<int>(type: "integer", nullable: false),
                    id_empresa = table.Column<int>(type: "integer", nullable: false),
                    data_inclusao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    data_alteracao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresas_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_empresas_usuarios_empresas_id_empresa",
                        column: x => x.id_empresa,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "anexos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_analise = table.Column<int>(type: "integer", nullable: false),
                    nome_arquivo = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    caminho = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    data_inclusao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    data_alteracao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anexos", x => x.id);
                    table.ForeignKey(
                        name: "FK_anexos_analises_id_analise",
                        column: x => x.id_analise,
                        principalTable: "analises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "empresas",
                columns: new[] { "id", "contrato_ativo", "data_alteracao", "data_inclusao", "nome_fantasia", "razao_social" },
                values: new object[,]
                {
                    { 1, true, null, new DateTimeOffset(new DateTime(2021, 1, 27, 15, 20, 24, 788, DateTimeKind.Unspecified).AddTicks(981), new TimeSpan(0, 0, 0, 0, 0)), "Quality - Consultoria de Processos", "Quality Consultorias LTDA" },
                    { 2, true, null, new DateTimeOffset(new DateTime(2021, 1, 27, 15, 20, 24, 788, DateTimeKind.Unspecified).AddTicks(1631), new TimeSpan(0, 0, 0, 0, 0)), "People - Consultoria de RH", "People Consultoria de RH Eireli" }
                });

            migrationBuilder.InsertData(
                table: "empresas_usuarios",
                columns: new[] { "id", "data_alteracao", "data_inclusao", "id_empresa", "id_usuario" },
                values: new object[,]
                {
                    { 1, null, new DateTimeOffset(new DateTime(2021, 1, 27, 15, 20, 24, 791, DateTimeKind.Unspecified).AddTicks(6999), new TimeSpan(0, 0, 0, 0, 0)), 1, 2 },
                    { 2, null, new DateTimeOffset(new DateTime(2021, 1, 27, 15, 20, 24, 791, DateTimeKind.Unspecified).AddTicks(7067), new TimeSpan(0, 0, 0, 0, 0)), 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_analises_id_empresa",
                table: "analises",
                column: "id_empresa");

            migrationBuilder.CreateIndex(
                name: "IX_anexos_id_analise",
                table: "anexos",
                column: "id_analise");

            migrationBuilder.CreateIndex(
                name: "IX_empresas_usuarios_id_empresa",
                table: "empresas_usuarios",
                column: "id_empresa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anexos");

            migrationBuilder.DropTable(
                name: "empresas_usuarios");

            migrationBuilder.DropTable(
                name: "analises");

            migrationBuilder.DropTable(
                name: "empresas");
        }
    }
}
