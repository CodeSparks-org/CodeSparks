using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSparks.Migrations
{
    /// <inheritdoc />
    public partial class AppMetadata_AddStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "AppMetadata",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AppMetadata",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "AppMetadata");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "AppMetadata",
                newName: "Updated");
        }
    }
}
