using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peret.Migrations;

/// <inheritdoc />
public partial class _20261126202442_AddCampaignStudyTables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "QuizQuestions",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                CreatedByUserId = table.Column<string>(type: "TEXT", nullable: false),
                BookSetId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                CampaignNumber = table.Column<int>(type: "INTEGER", nullable: false),
                Book = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Chapter = table.Column<int>(type: "INTEGER", nullable: false),
                QuestionText = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                CorrectAnswer = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                OptionB = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                OptionC = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                OptionD = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_QuizQuestions", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ReadingProgress",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                UserId = table.Column<string>(type: "TEXT", nullable: false),
                BookSetId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                CampaignNumber = table.Column<int>(type: "INTEGER", nullable: false),
                Book = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Chapter = table.Column<int>(type: "INTEGER", nullable: false),
                ReadAt = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ReadingProgress", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "StudyNotes",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                UserId = table.Column<string>(type: "TEXT", nullable: false),
                BookSetId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                CampaignNumber = table.Column<int>(type: "INTEGER", nullable: false),
                Book = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Chapter = table.Column<int>(type: "INTEGER", nullable: false),
                Verse = table.Column<int>(type: "INTEGER", nullable: true),
                Content = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_StudyNotes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "UserCampaigns",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                UserId = table.Column<string>(type: "TEXT", nullable: false),
                BookSetId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                CurrentCampaign = table.Column<int>(type: "INTEGER", nullable: false),
                LastBook = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                LastChapter = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserCampaigns", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "QuizQuestions");

        migrationBuilder.DropTable(
            name: "ReadingProgress");

        migrationBuilder.DropTable(
            name: "StudyNotes");

        migrationBuilder.DropTable(
            name: "UserCampaigns");
    }
}
