using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Playground.Migrations
{
    /// <inheritdoc />
    public partial class RmaComplexProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rma_Rma",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Rma_Type",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rma_Rma",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "Rma_Type",
                table: "Complaints");
        }
    }
}
