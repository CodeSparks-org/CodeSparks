using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSparks.Migrations
{
    /// <inheritdoc />
    public partial class AddHashtags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashtagList",
                table: "Sparks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Hashtags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SparkId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hashtags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hashtags_Sparks_SparkId",
                        column: x => x.SparkId,
                        principalTable: "Sparks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hashtags_SparkId",
                table: "Hashtags",
                column: "SparkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hashtags");

            migrationBuilder.DropColumn(
                name: "HashtagList",
                table: "Sparks");
        }
    }
}
