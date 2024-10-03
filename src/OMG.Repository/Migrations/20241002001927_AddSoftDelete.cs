using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMG.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Produtos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Produtos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Pedidos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Pedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "PedidoItens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PedidoItens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Formatos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Formatos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Cores",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Cores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Clientes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Aromas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Aromas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_IsDeleted",
                table: "Produtos",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IsDeleted",
                table: "Pedidos",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_IsDeleted",
                table: "PedidoItens",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Formatos_IsDeleted",
                table: "Formatos",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Cores_IsDeleted",
                table: "Cores",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_IsDeleted",
                table: "Clientes",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Aromas_IsDeleted",
                table: "Aromas",
                column: "IsDeleted",
                filter: "IsDeleted = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Produtos_IsDeleted",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_IsDeleted",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_PedidoItens_IsDeleted",
                table: "PedidoItens");

            migrationBuilder.DropIndex(
                name: "IX_Formatos_IsDeleted",
                table: "Formatos");

            migrationBuilder.DropIndex(
                name: "IX_Cores_IsDeleted",
                table: "Cores");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_IsDeleted",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Aromas_IsDeleted",
                table: "Aromas");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "PedidoItens");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PedidoItens");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Formatos");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Formatos");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Cores");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cores");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Aromas");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Aromas");
        }
    }
}
