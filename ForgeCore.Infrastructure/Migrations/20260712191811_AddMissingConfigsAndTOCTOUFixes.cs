using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForgeCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingConfigsAndTOCTOUFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthProviders_accounts_AccountId",
                table: "AuthProviders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthProviders",
                table: "AuthProviders");

            migrationBuilder.RenameTable(
                name: "Sessions",
                newName: "sessions");

            migrationBuilder.RenameTable(
                name: "AuthProviders",
                newName: "auth_providers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "sessions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "sessions",
                newName: "refresh_token");

            migrationBuilder.RenameColumn(
                name: "IpAddress",
                table: "sessions",
                newName: "ip_address");

            migrationBuilder.RenameColumn(
                name: "ExpiresAt",
                table: "sessions",
                newName: "expires_at");

            migrationBuilder.RenameColumn(
                name: "DeviceInfo",
                table: "sessions",
                newName: "device_info");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "sessions",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "sessions",
                newName: "account_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "currency_balances",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "auth_providers",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "auth_providers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "auth_providers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ProviderUserId",
                table: "auth_providers",
                newName: "provider_user_id");

            migrationBuilder.RenameColumn(
                name: "LinkedAt",
                table: "auth_providers",
                newName: "linked_at");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "auth_providers",
                newName: "account_id");

            migrationBuilder.RenameIndex(
                name: "IX_AuthProviders_AccountId",
                table: "auth_providers",
                newName: "IX_auth_providers_account_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sessions",
                table: "sessions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_auth_providers",
                table: "auth_providers",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_wallets_Id",
                table: "wallets",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sessions_id",
                table: "sessions",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_players_id",
                table: "players",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inventory_entries_id",
                table: "inventory_entries",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inventories_id",
                table: "inventories",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_accounts_display_name",
                table: "accounts",
                column: "display_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_accounts_email",
                table: "accounts",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_accounts_id",
                table: "accounts",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_auth_providers_id",
                table: "auth_providers",
                column: "id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_auth_providers_accounts_account_id",
                table: "auth_providers",
                column: "account_id",
                principalTable: "accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_auth_providers_accounts_account_id",
                table: "auth_providers");

            migrationBuilder.DropIndex(
                name: "IX_wallets_Id",
                table: "wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sessions",
                table: "sessions");

            migrationBuilder.DropIndex(
                name: "IX_sessions_id",
                table: "sessions");

            migrationBuilder.DropIndex(
                name: "IX_players_id",
                table: "players");

            migrationBuilder.DropIndex(
                name: "IX_inventory_entries_id",
                table: "inventory_entries");

            migrationBuilder.DropIndex(
                name: "IX_inventories_id",
                table: "inventories");

            migrationBuilder.DropIndex(
                name: "IX_accounts_display_name",
                table: "accounts");

            migrationBuilder.DropIndex(
                name: "IX_accounts_email",
                table: "accounts");

            migrationBuilder.DropIndex(
                name: "IX_accounts_id",
                table: "accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_auth_providers",
                table: "auth_providers");

            migrationBuilder.DropIndex(
                name: "IX_auth_providers_id",
                table: "auth_providers");

            migrationBuilder.RenameTable(
                name: "sessions",
                newName: "Sessions");

            migrationBuilder.RenameTable(
                name: "auth_providers",
                newName: "AuthProviders");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Sessions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "refresh_token",
                table: "Sessions",
                newName: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "ip_address",
                table: "Sessions",
                newName: "IpAddress");

            migrationBuilder.RenameColumn(
                name: "expires_at",
                table: "Sessions",
                newName: "ExpiresAt");

            migrationBuilder.RenameColumn(
                name: "device_info",
                table: "Sessions",
                newName: "DeviceInfo");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Sessions",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "account_id",
                table: "Sessions",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "currency_balances",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "AuthProviders",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "AuthProviders",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AuthProviders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "provider_user_id",
                table: "AuthProviders",
                newName: "ProviderUserId");

            migrationBuilder.RenameColumn(
                name: "linked_at",
                table: "AuthProviders",
                newName: "LinkedAt");

            migrationBuilder.RenameColumn(
                name: "account_id",
                table: "AuthProviders",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_auth_providers_account_id",
                table: "AuthProviders",
                newName: "IX_AuthProviders_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthProviders",
                table: "AuthProviders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthProviders_accounts_AccountId",
                table: "AuthProviders",
                column: "AccountId",
                principalTable: "accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
