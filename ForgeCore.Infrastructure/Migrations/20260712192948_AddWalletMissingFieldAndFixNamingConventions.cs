using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForgeCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWalletMissingFieldAndFixNamingConventions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "wallets",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "wallets",
                newName: "owner_id");

            migrationBuilder.RenameIndex(
                name: "IX_wallets_Id",
                table: "wallets",
                newName: "IX_wallets_id");

            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "players",
                newName: "nickname");

            migrationBuilder.RenameColumn(
                name: "LastActiveAt",
                table: "players",
                newName: "last_active_at");

            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "currency_balances",
                newName: "balance");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "currency_balances",
                newName: "currency_id");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "wallets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "wallets");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "wallets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "owner_id",
                table: "wallets",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_wallets_id",
                table: "wallets",
                newName: "IX_wallets_Id");

            migrationBuilder.RenameColumn(
                name: "nickname",
                table: "players",
                newName: "Nickname");

            migrationBuilder.RenameColumn(
                name: "last_active_at",
                table: "players",
                newName: "LastActiveAt");

            migrationBuilder.RenameColumn(
                name: "balance",
                table: "currency_balances",
                newName: "Balance");

            migrationBuilder.RenameColumn(
                name: "currency_id",
                table: "currency_balances",
                newName: "CurrencyId");
        }
    }
}
