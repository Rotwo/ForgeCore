using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForgeCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionRefreshTokenUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Session_RefreshToken_Unique",
                table: "sessions",
                column: "refresh_token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Session_RefreshToken_Unique",
                table: "sessions");
        }
    }
}
