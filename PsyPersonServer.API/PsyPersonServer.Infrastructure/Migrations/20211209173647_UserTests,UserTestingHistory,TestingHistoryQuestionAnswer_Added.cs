using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PsyPersonServer.Infrastructure.Migrations
{
    public partial class UserTestsUserTestingHistoryTestingHistoryQuestionAnswer_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsTested = table.Column<bool>(type: "bit", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTests_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTestingHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TestedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserTestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTestingHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTestingHistories_UserTests_UserTestId",
                        column: x => x.UserTestId,
                        principalTable: "UserTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestingHistoryQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsMarked = table.Column<bool>(type: "bit", nullable: false),
                    AnswerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserTestingHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingHistoryQuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestingHistoryQuestionAnswers_TestQuestionAnswers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "TestQuestionAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestingHistoryQuestionAnswers_UserTestingHistories_UserTestingHistoryId",
                        column: x => x.UserTestingHistoryId,
                        principalTable: "UserTestingHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestingHistoryQuestionAnswers_AnswerId",
                table: "TestingHistoryQuestionAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_TestingHistoryQuestionAnswers_UserTestingHistoryId",
                table: "TestingHistoryQuestionAnswers",
                column: "UserTestingHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTestingHistories_UserTestId",
                table: "UserTestingHistories",
                column: "UserTestId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTests_TestId",
                table: "UserTests",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTests_UserId",
                table: "UserTests",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestingHistoryQuestionAnswers");

            migrationBuilder.DropTable(
                name: "UserTestingHistories");

            migrationBuilder.DropTable(
                name: "UserTests");
        }
    }
}
