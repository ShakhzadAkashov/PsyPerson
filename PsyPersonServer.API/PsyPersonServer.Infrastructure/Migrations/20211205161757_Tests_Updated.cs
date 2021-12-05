using Microsoft.EntityFrameworkCore.Migrations;

namespace PsyPersonServer.Infrastructure.Migrations
{
    public partial class Tests_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestType",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestType",
                table: "Tests");
        }
    }
}
