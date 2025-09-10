using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace viaentrega.site.Migrations
{
    /// <inheritdoc />
    public partial class ajustes_usuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PessoaId",
                table: "Usuarios",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PessoaId",
                table: "Usuarios");
        }
    }
}
