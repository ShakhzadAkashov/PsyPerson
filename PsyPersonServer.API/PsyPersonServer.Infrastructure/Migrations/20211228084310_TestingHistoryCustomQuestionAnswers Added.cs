using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PsyPersonServer.Infrastructure.Migrations
{
    public partial class TestingHistoryCustomQuestionAnswersAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestingHistoryCustomQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerScore = table.Column<double>(type: "float", nullable: false),
                    AnswerStatus = table.Column<int>(type: "int", nullable: false),
                    UserTestingHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingHistoryCustomQuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestingHistoryCustomQuestionAnswers_UserTestingHistories_UserTestingHistoryId",
                        column: x => x.UserTestingHistoryId,
                        principalTable: "UserTestingHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestingHistoryCustomQuestionAnswers_UserTestingHistoryId",
                table: "TestingHistoryCustomQuestionAnswers",
                column: "UserTestingHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestingHistoryCustomQuestionAnswers");
        }
    }
}
