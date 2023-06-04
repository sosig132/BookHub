using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End.Migrations
{
    /// <inheritdoc />
    public partial class FinalMigration2111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5331a370-5901-4af9-a141-b4f08565583f"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DisplayName", "Email", "IsBanned", "Password", "Role", "Username" },
                values: new object[] { new Guid("5713b9e4-39ef-4fdc-934c-7aff823b7548"), "admin", "sosig132@gmail.com", null, "$2a$11$I57iAxBcWI2nLsNkLufso.zQrm0HgAh14MfsxJXzfyRpXbi7LsZRO", 0, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5713b9e4-39ef-4fdc-934c-7aff823b7548"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DisplayName", "Email", "IsBanned", "Password", "Role", "Username" },
                values: new object[] { new Guid("5331a370-5901-4af9-a141-b4f08565583f"), "admin", "sosig132@gmail.com", null, "$2a$11$anipN/JDspPDRMAG/0IsKeUi4d73xYwtg0pKfnz0tAqJ/Y0iPkFES", 0, "admin" });
        }
    }
}
