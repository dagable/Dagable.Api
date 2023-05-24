using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dagable.DataAccess.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class localGraphNodeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GraphNodeId",
                schema: "Dagable",
                table: "Node",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GraphNodeId",
                schema: "Dagable",
                table: "Node");
        }
    }
}
