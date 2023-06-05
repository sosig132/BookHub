using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End.Migrations
{
    /// <inheritdoc />
    public partial class Ge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5713b9e4-39ef-4fdc-934c-7aff823b7548"));

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DisplayName", "Email", "IsBanned", "Password", "Role", "Username" },
                values: new object[] { new Guid("1f6bd821-1c53-4482-adbe-116bd5084818"), "admin", "sosig132@gmail.com", null, "$2a$11$pz5xjBC5K2y/FXn2MSf5SuM0W24K2fpzHngq9ZUTm12m/CpczWmyS", 0, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1f6bd821-1c53-4482-adbe-116bd5084818"));

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Books",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DisplayName", "Email", "IsBanned", "Password", "Role", "Username" },
                values: new object[] { new Guid("5713b9e4-39ef-4fdc-934c-7aff823b7548"), "admin", "sosig132@gmail.com", null, "$2a$11$I57iAxBcWI2nLsNkLufso.zQrm0HgAh14MfsxJXzfyRpXbi7LsZRO", 0, "admin" });
        }
    }
}
