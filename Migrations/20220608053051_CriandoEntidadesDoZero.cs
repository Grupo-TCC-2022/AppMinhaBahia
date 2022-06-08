using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppMinhaBahia.Migrations
{
    public partial class CriandoEntidadesDoZero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prefeituras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeCidade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prefeituras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Fundos = table.Column<double>(nullable: false),
                    PrefeituraId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setor_Prefeituras_PrefeituraId",
                        column: x => x.PrefeituraId,
                        principalTable: "Prefeituras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requisicao",
                columns: table => new
                {
                    SetorId = table.Column<int>(nullable: false),
                    PrefeituraId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Tipo = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    Fundos = table.Column<double>(nullable: true),
                    Funcionarios = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requisicao", x => new { x.SetorId, x.PrefeituraId });
                    table.ForeignKey(
                        name: "FK_Requisicao_Prefeituras_PrefeituraId",
                        column: x => x.PrefeituraId,
                        principalTable: "Prefeituras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requisicao_Setor_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeCompleto = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    SetorId = table.Column<int>(nullable: true),
                    Disponivel = table.Column<bool>(nullable: true),
                    IntervencaoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Setor_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ocorrencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: false),
                    LocalEspecifico = table.Column<string>(nullable: true),
                    Custo = table.Column<double>(nullable: true),
                    Funcionarios = table.Column<int>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    PrefeituraId = table.Column<int>(nullable: true),
                    SetorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocorrencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ocorrencia_Prefeituras_PrefeituraId",
                        column: x => x.PrefeituraId,
                        principalTable: "Prefeituras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ocorrencia_Setor_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ocorrencia_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Intervencao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    OcorrenciaId = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intervencao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intervencao_Ocorrencia_OcorrenciaId",
                        column: x => x.OcorrenciaId,
                        principalTable: "Ocorrencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Intervencao_OcorrenciaId",
                table: "Intervencao",
                column: "OcorrenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencia_PrefeituraId",
                table: "Ocorrencia",
                column: "PrefeituraId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencia_SetorId",
                table: "Ocorrencia",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencia_UsuarioId",
                table: "Ocorrencia",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisicao_PrefeituraId",
                table: "Requisicao",
                column: "PrefeituraId");

            migrationBuilder.CreateIndex(
                name: "IX_Setor_PrefeituraId",
                table: "Setor",
                column: "PrefeituraId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IntervencaoId",
                table: "Usuario",
                column: "IntervencaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_SetorId",
                table: "Usuario",
                column: "SetorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Intervencao_IntervencaoId",
                table: "Usuario",
                column: "IntervencaoId",
                principalTable: "Intervencao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intervencao_Ocorrencia_OcorrenciaId",
                table: "Intervencao");

            migrationBuilder.DropTable(
                name: "Requisicao");

            migrationBuilder.DropTable(
                name: "Ocorrencia");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Intervencao");

            migrationBuilder.DropTable(
                name: "Setor");

            migrationBuilder.DropTable(
                name: "Prefeituras");
        }
    }
}
