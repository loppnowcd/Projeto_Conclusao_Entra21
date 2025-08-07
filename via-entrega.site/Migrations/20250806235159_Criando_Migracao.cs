using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace viaentrega.site.Migrations
{
    /// <inheritdoc />
    public partial class Criando_Migracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MarcaFipe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AnoFabricacao = table.Column<int>(type: "int", nullable: false),
                    Cor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Placa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VeiculoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TipoPessoa = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    NomeMae = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Sexo = table.Column<int>(type: "int", nullable: true),
                    Cor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstadoCivil = table.Column<int>(type: "int", nullable: true),
                    LicenseNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NomeFatasia = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Cnpj = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pessoas_Veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Pessoas_Veiculos_VeiculoId1",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CollectionOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PessoaFisicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PessoaJuridicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Destinatario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionOrders_Pessoas_PessoaFisicaId",
                        column: x => x.PessoaFisicaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollectionOrders_Pessoas_PessoaJuridicaId",
                        column: x => x.PessoaJuridicaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DadosContatos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PessoaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PessoaId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosContatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DadosContatos_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DadosContatos_Pessoas_PessoaId1",
                        column: x => x.PessoaId1,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DadosEnderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PessoaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosEnderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DadosEnderecos_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PessoaFisicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PessoaJuridicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CollectionOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DataEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataColeta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Imagem = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryOrders_CollectionOrders_CollectionOrderId",
                        column: x => x.CollectionOrderId,
                        principalTable: "CollectionOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryOrders_Pessoas_PessoaFisicaId",
                        column: x => x.PessoaFisicaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryOrders_Pessoas_PessoaJuridicaId",
                        column: x => x.PessoaJuridicaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Altura = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Largura = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comprimento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Peso = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Imagem = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CollectionOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_CollectionOrders_CollectionOrderId",
                        column: x => x.CollectionOrderId,
                        principalTable: "CollectionOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionOrders_PessoaFisicaId",
                table: "CollectionOrders",
                column: "PessoaFisicaId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionOrders_PessoaJuridicaId",
                table: "CollectionOrders",
                column: "PessoaJuridicaId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosContatos_PessoaId",
                table: "DadosContatos",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosContatos_PessoaId1",
                table: "DadosContatos",
                column: "PessoaId1");

            migrationBuilder.CreateIndex(
                name: "IX_DadosEnderecos_PessoaId",
                table: "DadosEnderecos",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrders_CollectionOrderId",
                table: "DeliveryOrders",
                column: "CollectionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrders_PessoaFisicaId",
                table: "DeliveryOrders",
                column: "PessoaFisicaId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrders_PessoaJuridicaId",
                table: "DeliveryOrders",
                column: "PessoaJuridicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_VeiculoId",
                table: "Pessoas",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CollectionOrderId",
                table: "Products",
                column: "CollectionOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DadosContatos");

            migrationBuilder.DropTable(
                name: "DadosEnderecos");

            migrationBuilder.DropTable(
                name: "DeliveryOrders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "CollectionOrders");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "Veiculos");
        }
    }
}
