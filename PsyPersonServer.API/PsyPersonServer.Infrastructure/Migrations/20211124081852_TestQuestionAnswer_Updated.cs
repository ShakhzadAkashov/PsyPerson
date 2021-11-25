using Microsoft.EntityFrameworkCore.Migrations;

namespace PsyPersonServer.Infrastructure.Migrations
{
    public partial class TestQuestionAnswer_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdForView",
                table: "TestQuestionAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdForView",
                table: "TestQuestionAnswers");
        }
    }
}
