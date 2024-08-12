using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSparks.Migrations
{
    /// <inheritdoc />
    public partial class Rename_QuestProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestProgresses_AspNetUsers_UserId",
                table: "QuestProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestProgresses_Quests_QuestId",
                table: "QuestProgresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestProgresses",
                table: "QuestProgresses");

            migrationBuilder.RenameTable(
                name: "QuestProgresses",
                newName: "QuestProgress");

            migrationBuilder.RenameIndex(
                name: "IX_QuestProgresses_UserId",
                table: "QuestProgress",
                newName: "IX_QuestProgress_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestProgresses_QuestId",
                table: "QuestProgress",
                newName: "IX_QuestProgress_QuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestProgress",
                table: "QuestProgress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestProgress_AspNetUsers_UserId",
                table: "QuestProgress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestProgress_Quests_QuestId",
                table: "QuestProgress",
                column: "QuestId",
                principalTable: "Quests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestProgress_AspNetUsers_UserId",
                table: "QuestProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestProgress_Quests_QuestId",
                table: "QuestProgress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestProgress",
                table: "QuestProgress");

            migrationBuilder.RenameTable(
                name: "QuestProgress",
                newName: "QuestProgresses");

            migrationBuilder.RenameIndex(
                name: "IX_QuestProgress_UserId",
                table: "QuestProgresses",
                newName: "IX_QuestProgresses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestProgress_QuestId",
                table: "QuestProgresses",
                newName: "IX_QuestProgresses_QuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestProgresses",
                table: "QuestProgresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestProgresses_AspNetUsers_UserId",
                table: "QuestProgresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestProgresses_Quests_QuestId",
                table: "QuestProgresses",
                column: "QuestId",
                principalTable: "Quests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
