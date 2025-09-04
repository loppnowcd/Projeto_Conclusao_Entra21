using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace viaentrega.site.Migrations
{
    /// <inheritdoc />
    public partial class ajuste_configuracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DadosContatos_Pessoas_PessoaId1",
                table: "DadosContatos");

            migrationBuilder.DropIndex(
                name: "IX_DadosContatos_PessoaId1",
                table: "DadosContatos");

            migrationBuilder.DropColumn(
                name: "PessoaId1",
                table: "DadosContatos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PessoaId1",
                table: "DadosContatos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DadosContatos_PessoaId1",
                table: "DadosContatos",
                column: "PessoaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosContatos_Pessoas_PessoaId1",
                table: "DadosContatos",
                column: "PessoaId1",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
