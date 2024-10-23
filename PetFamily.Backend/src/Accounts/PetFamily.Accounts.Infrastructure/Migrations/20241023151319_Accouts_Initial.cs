using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Accouts_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_participant_account_asp_net_users_user_id",
                schema: "accounts",
                table: "participant_account");

            migrationBuilder.DropForeignKey(
                name: "fk_volunteer_account_asp_net_users_user_id",
                schema: "accounts",
                table: "volunteer_account");

            migrationBuilder.DropPrimaryKey(
                name: "pk_volunteer_account",
                schema: "accounts",
                table: "volunteer_account");

            migrationBuilder.DropPrimaryKey(
                name: "pk_participant_account",
                schema: "accounts",
                table: "participant_account");

            migrationBuilder.RenameTable(
                name: "volunteer_account",
                schema: "accounts",
                newName: "volunteers",
                newSchema: "accounts");

            migrationBuilder.RenameTable(
                name: "participant_account",
                schema: "accounts",
                newName: "participant_accounts",
                newSchema: "accounts");

            migrationBuilder.RenameIndex(
                name: "ix_volunteer_account_user_id",
                schema: "accounts",
                table: "volunteers",
                newName: "ix_volunteers_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_participant_account_user_id",
                schema: "accounts",
                table: "participant_accounts",
                newName: "ix_participant_accounts_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_volunteers",
                schema: "accounts",
                table: "volunteers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_participant_accounts",
                schema: "accounts",
                table: "participant_accounts",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_participant_accounts_asp_net_users_user_id",
                schema: "accounts",
                table: "participant_accounts",
                column: "user_id",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_volunteers_users_user_id",
                schema: "accounts",
                table: "volunteers",
                column: "user_id",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_participant_accounts_asp_net_users_user_id",
                schema: "accounts",
                table: "participant_accounts");

            migrationBuilder.DropForeignKey(
                name: "fk_volunteers_users_user_id",
                schema: "accounts",
                table: "volunteers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_volunteers",
                schema: "accounts",
                table: "volunteers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_participant_accounts",
                schema: "accounts",
                table: "participant_accounts");

            migrationBuilder.RenameTable(
                name: "volunteers",
                schema: "accounts",
                newName: "volunteer_account",
                newSchema: "accounts");

            migrationBuilder.RenameTable(
                name: "participant_accounts",
                schema: "accounts",
                newName: "participant_account",
                newSchema: "accounts");

            migrationBuilder.RenameIndex(
                name: "ix_volunteers_user_id",
                schema: "accounts",
                table: "volunteer_account",
                newName: "ix_volunteer_account_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_participant_accounts_user_id",
                schema: "accounts",
                table: "participant_account",
                newName: "ix_participant_account_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_volunteer_account",
                schema: "accounts",
                table: "volunteer_account",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_participant_account",
                schema: "accounts",
                table: "participant_account",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_participant_account_asp_net_users_user_id",
                schema: "accounts",
                table: "participant_account",
                column: "user_id",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_volunteer_account_asp_net_users_user_id",
                schema: "accounts",
                table: "volunteer_account",
                column: "user_id",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
