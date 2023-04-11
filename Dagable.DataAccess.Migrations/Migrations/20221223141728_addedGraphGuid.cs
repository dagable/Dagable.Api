using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dagable.DataAccess.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class addedGraphGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GraphGuid",
                schema: "Dagable",
                table: "Graph",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GraphGuid",
                schema: "Dagable",
                table: "Graph");
        }
    }
}
