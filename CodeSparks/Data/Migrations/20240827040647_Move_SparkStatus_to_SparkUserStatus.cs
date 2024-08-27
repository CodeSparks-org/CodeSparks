using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSparks.Migrations
{
    /// <inheritdoc />
    public partial class Move_SparkStatus_to_SparkUserStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sparks");

            migrationBuilder.CreateTable(
                name: "SparkUserStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SparkId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StartedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReviewedById = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparkUserStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SparkUserStatus_AspNetUsers_ReviewedById",
                        column: x => x.ReviewedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SparkUserStatus_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SparkUserStatus_Sparks_SparkId",
                        column: x => x.SparkId,
                        principalTable: "Sparks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SparkUserStatus_ReviewedById",
                table: "SparkUserStatus",
                column: "ReviewedById");

            migrationBuilder.CreateIndex(
                name: "IX_SparkUserStatus_SparkId",
                table: "SparkUserStatus",
                column: "SparkId");

            migrationBuilder.CreateIndex(
                name: "IX_SparkUserStatus_UserId",
                table: "SparkUserStatus",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SparkUserStatus");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Sparks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
