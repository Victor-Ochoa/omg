using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMG.Repository.Migrations;

/// <inheritdoc />
public partial class addEventChangeStatus : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "EventChangeStatus",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                IdPedido = table.Column<int>(type: "int", nullable: false),
                OldStatus = table.Column<int>(type: "int", maxLength: 250, nullable: false),
                NewStatus = table.Column<int>(type: "int", nullable: false),
                DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EventChangeStatus", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "EventChangeStatus");
    }
}
