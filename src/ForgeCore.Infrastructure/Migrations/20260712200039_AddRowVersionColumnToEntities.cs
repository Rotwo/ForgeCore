using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForgeCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRowVersionColumnToEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "row_version",
                table: "wallets",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "row_version",
                table: "sessions",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "row_version",
                table: "players",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "row_version",
                table: "inventory_entries",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "row_version",
                table: "inventories",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "currency_balances",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "row_version",
                table: "accounts",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "row_version",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "row_version",
                table: "sessions");

            migrationBuilder.DropColumn(
                name: "row_version",
                table: "players");

            migrationBuilder.DropColumn(
                name: "row_version",
                table: "inventory_entries");

            migrationBuilder.DropColumn(
                name: "row_version",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "currency_balances");

            migrationBuilder.DropColumn(
                name: "row_version",
                table: "accounts");
        }
    }
}
