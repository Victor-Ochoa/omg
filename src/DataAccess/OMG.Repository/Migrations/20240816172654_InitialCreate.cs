using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMG.Repository.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Aromas",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nome = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Aromas", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Clientes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                Telefone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Endereco = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Clientes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Cores",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nome = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Cores", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Formatos",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Descricao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Formatos", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Produtos",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Descricao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Produtos", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Pedidos",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Status = table.Column<int>(type: "int", nullable: false),
                ClienteId = table.Column<int>(type: "int", nullable: false),
                ValorTotal = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                Desconto = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                Entrada = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                IsPermuta = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                DataEntrega = table.Column<DateOnly>(type: "date", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Pedidos", x => x.Id);
                table.ForeignKey(
                    name: "FK_Pedidos_Clientes_ClienteId",
                    column: x => x.ClienteId,
                    principalTable: "Clientes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PedidoItens",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PedidoId = table.Column<int>(type: "int", nullable: false),
                ProdutoId = table.Column<int>(type: "int", nullable: false),
                FormatoId = table.Column<int>(type: "int", nullable: false),
                CorId = table.Column<int>(type: "int", nullable: false),
                AromaId = table.Column<int>(type: "int", nullable: false),
                Quantidade = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PedidoItens", x => x.Id);
                table.ForeignKey(
                    name: "FK_PedidoItens_Aromas_AromaId",
                    column: x => x.AromaId,
                    principalTable: "Aromas",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PedidoItens_Cores_CorId",
                    column: x => x.CorId,
                    principalTable: "Cores",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PedidoItens_Formatos_FormatoId",
                    column: x => x.FormatoId,
                    principalTable: "Formatos",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PedidoItens_Pedidos_PedidoId",
                    column: x => x.PedidoId,
                    principalTable: "Pedidos",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PedidoItens_Produtos_ProdutoId",
                    column: x => x.ProdutoId,
                    principalTable: "Produtos",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Aromas_Nome",
            table: "Aromas",
            column: "Nome");

        migrationBuilder.CreateIndex(
            name: "IX_Clientes_Nome",
            table: "Clientes",
            column: "Nome");

        migrationBuilder.CreateIndex(
            name: "IX_Cores_Nome",
            table: "Cores",
            column: "Nome");

        migrationBuilder.CreateIndex(
            name: "IX_Formatos_Descricao",
            table: "Formatos",
            column: "Descricao");

        migrationBuilder.CreateIndex(
            name: "IX_PedidoItens_AromaId",
            table: "PedidoItens",
            column: "AromaId");

        migrationBuilder.CreateIndex(
            name: "IX_PedidoItens_CorId",
            table: "PedidoItens",
            column: "CorId");

        migrationBuilder.CreateIndex(
            name: "IX_PedidoItens_FormatoId",
            table: "PedidoItens",
            column: "FormatoId");

        migrationBuilder.CreateIndex(
            name: "IX_PedidoItens_PedidoId",
            table: "PedidoItens",
            column: "PedidoId");

        migrationBuilder.CreateIndex(
            name: "IX_PedidoItens_ProdutoId",
            table: "PedidoItens",
            column: "ProdutoId");

        migrationBuilder.CreateIndex(
            name: "IX_Pedidos_ClienteId",
            table: "Pedidos",
            column: "ClienteId");

        migrationBuilder.CreateIndex(
            name: "IX_Produtos_Descricao",
            table: "Produtos",
            column: "Descricao");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PedidoItens");

        migrationBuilder.DropTable(
            name: "Aromas");

        migrationBuilder.DropTable(
            name: "Cores");

        migrationBuilder.DropTable(
            name: "Formatos");

        migrationBuilder.DropTable(
            name: "Pedidos");

        migrationBuilder.DropTable(
            name: "Produtos");

        migrationBuilder.DropTable(
            name: "Clientes");
    }
}
