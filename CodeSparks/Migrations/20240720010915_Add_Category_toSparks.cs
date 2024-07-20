using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSparks.Migrations
{
    /// <inheritdoc />
    public partial class Add_Category_toSparks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Sparks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Sparks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Sparks");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Sparks");
        }
    }
}
