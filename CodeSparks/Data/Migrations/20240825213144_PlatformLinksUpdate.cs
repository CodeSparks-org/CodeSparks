using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSparks.Migrations
{
    /// <inheritdoc />
    public partial class PlatformLinksUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlatformLinks_AspNetUsers_UserId",
                table: "PlatformLinks");

            migrationBuilder.DropIndex(
                name: "IX_PlatformLinks_UserId",
                table: "PlatformLinks");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "PlatformLinks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlatformLinks_AppUserId",
                table: "PlatformLinks",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlatformLinks_AspNetUsers_AppUserId",
                table: "PlatformLinks",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlatformLinks_AspNetUsers_AppUserId",
                table: "PlatformLinks");

            migrationBuilder.DropIndex(
                name: "IX_PlatformLinks_AppUserId",
                table: "PlatformLinks");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "PlatformLinks");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformLinks_UserId",
                table: "PlatformLinks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlatformLinks_AspNetUsers_UserId",
                table: "PlatformLinks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
