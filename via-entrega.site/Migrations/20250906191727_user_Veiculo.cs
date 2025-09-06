using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace viaentrega.site.Migrations
{
    /// <inheritdoc />
    public partial class user_Veiculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Veiculos_VeiculoId",
                table: "Pessoas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Veiculos_VeiculoId1",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_VeiculoId",
                table: "Pessoas");

            migrationBuilder.AddColumn<Guid>(
                name: "PessoaId",
                table: "Veiculos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_PessoaId",
                table: "Veiculos",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_Pessoas_PessoaId",
                table: "Veiculos",
                column: "PessoaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_Pessoas_PessoaId",
                table: "Veiculos");

            migrationBuilder.DropIndex(
                name: "IX_Veiculos_PessoaId",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "PessoaId",
                table: "Veiculos");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_VeiculoId",
                table: "Pessoas",
                column: "VeiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Veiculos_VeiculoId",
                table: "Pessoas",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Veiculos_VeiculoId1",
                table: "Pessoas",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "Id");
        }
    }
}
