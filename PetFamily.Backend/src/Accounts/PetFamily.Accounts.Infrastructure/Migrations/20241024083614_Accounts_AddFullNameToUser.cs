using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Accounts_AddFullNameToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                schema: "accounts",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "patronymic",
                schema: "accounts",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "surname",
                schema: "accounts",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "accounts",
                table: "participant_accounts");

            migrationBuilder.DropColumn(
                name: "patronymic",
                schema: "accounts",
                table: "participant_accounts");

            migrationBuilder.DropColumn(
                name: "surname",
                schema: "accounts",
                table: "participant_accounts");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "accounts",
                table: "admin_accounts");

            migrationBuilder.DropColumn(
                name: "patronymic",
                schema: "accounts",
                table: "admin_accounts");

            migrationBuilder.DropColumn(
                name: "surname",
                schema: "accounts",
                table: "admin_accounts");

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "accounts",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "patronymic",
                schema: "accounts",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "surname",
                schema: "accounts",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                schema: "accounts",
                table: "users");

            migrationBuilder.DropColumn(
                name: "patronymic",
                schema: "accounts",
                table: "users");

            migrationBuilder.DropColumn(
                name: "surname",
                schema: "accounts",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "accounts",
                table: "volunteers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "patronymic",
                schema: "accounts",
                table: "volunteers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "surname",
                schema: "accounts",
                table: "volunteers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "accounts",
                table: "participant_accounts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "patronymic",
                schema: "accounts",
                table: "participant_accounts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "surname",
                schema: "accounts",
                table: "participant_accounts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "accounts",
                table: "admin_accounts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "patronymic",
                schema: "accounts",
                table: "admin_accounts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "surname",
                schema: "accounts",
                table: "admin_accounts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
