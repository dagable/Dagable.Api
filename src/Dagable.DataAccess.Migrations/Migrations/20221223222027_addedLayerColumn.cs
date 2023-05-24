using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dagable.DataAccess.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class addedLayerColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Layer",
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
                name: "Layer",
                schema: "Dagable",
                table: "Node");
        }
    }
}
