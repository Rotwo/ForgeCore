using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForgeCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RestoreInventoryEntryMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "metadata",
                table: "inventory_entries",
                type: "jsonb",
                nullable: false,
                defaultValue: "{}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "metadata",
                table: "inventory_entries");
        }
    }
}
