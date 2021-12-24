using Microsoft.EntityFrameworkCore.Migrations;

namespace PsyPersonServer.Infrastructure.Migrations
{
    public partial class TestQuestion_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionType",
                table: "TestQuestions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionType",
                table: "TestQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
