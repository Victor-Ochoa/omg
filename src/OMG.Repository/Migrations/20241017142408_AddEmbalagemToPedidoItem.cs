using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMG.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddEmbalagemToPedidoItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmbalagemId",
                table: "PedidoItens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Embalagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Embalagens", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_EmbalagemId",
                table: "PedidoItens",
                column: "EmbalagemId");

            migrationBuilder.CreateIndex(
                name: "IX_Embalagens_Descricao",
                table: "Embalagens",
                column: "Descricao");

            migrationBuilder.CreateIndex(
                name: "IX_Embalagens_IsDeleted",
                table: "Embalagens",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItens_Embalagens_EmbalagemId",
                table: "PedidoItens",
                column: "EmbalagemId",
                principalTable: "Embalagens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItens_Embalagens_EmbalagemId",
                table: "PedidoItens");

            migrationBuilder.DropTable(
                name: "Embalagens");

            migrationBuilder.DropIndex(
                name: "IX_PedidoItens_EmbalagemId",
                table: "PedidoItens");

            migrationBuilder.DropColumn(
                name: "EmbalagemId",
                table: "PedidoItens");
        }
    }
}
