using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForgeCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCredentialsHashToAuthProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RowVersion",
                table: "currency_balances",
                newName: "row_version");

            migrationBuilder.AddColumn<string>(
                name: "credentials_hash",
                table: "auth_providers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "credentials_hash",
                table: "auth_providers");

            migrationBuilder.RenameColumn(
                name: "row_version",
                table: "currency_balances",
                newName: "RowVersion");
        }
    }
}
