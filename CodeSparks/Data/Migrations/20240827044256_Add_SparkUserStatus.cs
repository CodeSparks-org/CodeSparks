using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSparks.Migrations
{
    /// <inheritdoc />
    public partial class Add_SparkUserStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SparkUserStatus_AspNetUsers_ReviewedById",
                table: "SparkUserStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_SparkUserStatus_AspNetUsers_UserId",
                table: "SparkUserStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_SparkUserStatus_Sparks_SparkId",
                table: "SparkUserStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SparkUserStatus",
                table: "SparkUserStatus");

            migrationBuilder.RenameTable(
                name: "SparkUserStatus",
                newName: "SparkStatuses");

            migrationBuilder.RenameIndex(
                name: "IX_SparkUserStatus_UserId",
                table: "SparkStatuses",
                newName: "IX_SparkStatuses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SparkUserStatus_SparkId",
                table: "SparkStatuses",
                newName: "IX_SparkStatuses_SparkId");

            migrationBuilder.RenameIndex(
                name: "IX_SparkUserStatus_ReviewedById",
                table: "SparkStatuses",
                newName: "IX_SparkStatuses_ReviewedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SparkStatuses",
                table: "SparkStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SparkStatuses_AspNetUsers_ReviewedById",
                table: "SparkStatuses",
                column: "ReviewedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SparkStatuses_AspNetUsers_UserId",
                table: "SparkStatuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SparkStatuses_Sparks_SparkId",
                table: "SparkStatuses",
                column: "SparkId",
                principalTable: "Sparks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SparkStatuses_AspNetUsers_ReviewedById",
                table: "SparkStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SparkStatuses_AspNetUsers_UserId",
                table: "SparkStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SparkStatuses_Sparks_SparkId",
                table: "SparkStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SparkStatuses",
                table: "SparkStatuses");

            migrationBuilder.RenameTable(
                name: "SparkStatuses",
                newName: "SparkUserStatus");

            migrationBuilder.RenameIndex(
                name: "IX_SparkStatuses_UserId",
                table: "SparkUserStatus",
                newName: "IX_SparkUserStatus_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SparkStatuses_SparkId",
                table: "SparkUserStatus",
                newName: "IX_SparkUserStatus_SparkId");

            migrationBuilder.RenameIndex(
                name: "IX_SparkStatuses_ReviewedById",
                table: "SparkUserStatus",
                newName: "IX_SparkUserStatus_ReviewedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SparkUserStatus",
                table: "SparkUserStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SparkUserStatus_AspNetUsers_ReviewedById",
                table: "SparkUserStatus",
                column: "ReviewedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SparkUserStatus_AspNetUsers_UserId",
                table: "SparkUserStatus",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SparkUserStatus_Sparks_SparkId",
                table: "SparkUserStatus",
                column: "SparkId",
                principalTable: "Sparks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
