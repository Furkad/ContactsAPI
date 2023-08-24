using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactsAPI.Migrations
{
    /// <inheritdoc />
    public partial class BackConnect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Accounts_AccountLogin_AccountPassword",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Contacts_ContactId",
                table: "Items");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountPassword",
                table: "Contacts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountLogin",
                table: "Contacts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Accounts_AccountLogin_AccountPassword",
                table: "Contacts",
                columns: new[] { "AccountLogin", "AccountPassword" },
                principalTable: "Accounts",
                principalColumns: new[] { "Login", "Password" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Contacts_ContactId",
                table: "Items",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Accounts_AccountLogin_AccountPassword",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Contacts_ContactId",
                table: "Items");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "AccountPassword",
                table: "Contacts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AccountLogin",
                table: "Contacts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Accounts_AccountLogin_AccountPassword",
                table: "Contacts",
                columns: new[] { "AccountLogin", "AccountPassword" },
                principalTable: "Accounts",
                principalColumns: new[] { "Login", "Password" });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Contacts_ContactId",
                table: "Items",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");
        }
    }
}
