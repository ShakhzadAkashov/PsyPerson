using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PsyPersonServer.Infrastructure.Migrations
{
    public partial class TestingHistoryCustomQuestionAnswer_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TestQuestionId",
                table: "TestingHistoryCustomQuestionAnswers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TestingHistoryCustomQuestionAnswers_TestQuestionId",
                table: "TestingHistoryCustomQuestionAnswers",
                column: "TestQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestingHistoryCustomQuestionAnswers_TestQuestions_TestQuestionId",
                table: "TestingHistoryCustomQuestionAnswers",
                column: "TestQuestionId",
                principalTable: "TestQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestingHistoryCustomQuestionAnswers_TestQuestions_TestQuestionId",
                table: "TestingHistoryCustomQuestionAnswers");

            migrationBuilder.DropIndex(
                name: "IX_TestingHistoryCustomQuestionAnswers_TestQuestionId",
                table: "TestingHistoryCustomQuestionAnswers");

            migrationBuilder.DropColumn(
                name: "TestQuestionId",
                table: "TestingHistoryCustomQuestionAnswers");
        }
    }
}
