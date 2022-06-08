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
                name: "Intervencao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intervencao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeCompleto = table.Column<string>(maxLength: 50, nullable: false),
                    NomeCidade = table.Column<string>(maxLength: 50, nullable: false),
                    CPF = table.Column<string>(nullable: false),
                    Senha = table.Column<string>(maxLength: 50, nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    SetorId = table.Column<int>(nullable: true),
                    Disponivel = table.Column<bool>(nullable: true),
                    IntervencaoId = table.Column<int>(nullable: true),
                    VerbaEstadual = table.Column<double>(nullable: true),
                    SalarioMedioPorFuncionario = table.Column<double>(nullable: true),
                    NomeSetor = table.Column<string>(nullable: true),
                    Verba = table.Column<double>(nullable: true),
                    PrefeituraId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Intervencao_IntervencaoId",
                        column: x => x.IntervencaoId,
                        principalTable: "Intervencao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuarios_Usuarios_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_Usuarios_PrefeituraId",
                        column: x => x.PrefeituraId,
                        principalTable: "Usuarios",
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
                    IntervencaoId = table.Column<int>(nullable: false),
                    PrefeituraId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocorrencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ocorrencia_Intervencao_IntervencaoId",
                        column: x => x.IntervencaoId,
                        principalTable: "Intervencao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ocorrencia_Usuarios_PrefeituraId",
                        column: x => x.PrefeituraId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ocorrencia_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
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
                    Verba = table.Column<double>(nullable: true),
                    Funcionarios = table.Column<int>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requisicao", x => new { x.SetorId, x.PrefeituraId });
                    table.ForeignKey(
                        name: "FK_Requisicao_Usuarios_PrefeituraId",
                        column: x => x.PrefeituraId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requisicao_Usuarios_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencia_IntervencaoId",
                table: "Ocorrencia",
                column: "IntervencaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencia_PrefeituraId",
                table: "Ocorrencia",
                column: "PrefeituraId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencia_UsuarioId",
                table: "Ocorrencia",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisicao_PrefeituraId",
                table: "Requisicao",
                column: "PrefeituraId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IntervencaoId",
                table: "Usuarios",
                column: "IntervencaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SetorId",
                table: "Usuarios",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PrefeituraId",
                table: "Usuarios",
                column: "PrefeituraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ocorrencia");

            migrationBuilder.DropTable(
                name: "Requisicao");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Intervencao");
        }
    }
}
