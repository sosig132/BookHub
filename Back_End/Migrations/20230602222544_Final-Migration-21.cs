using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End.Migrations
{
    /// <inheritdoc />
    public partial class FinalMigration21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9e94de4c-ad25-4430-9c0c-27b1787690bd"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DisplayName", "Email", "IsBanned", "Password", "Role", "Username" },
                values: new object[] { new Guid("657e769e-034d-4107-a25c-57da8cfae32f"), "admin", "sosig132@gmail.com", null, "$2a$11$lREBIqXxXDet803Sy2W29.s81KKpYT7oTcA2d0CixUwyQyPnrwVaC", 0, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("657e769e-034d-4107-a25c-57da8cfae32f"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DisplayName", "Email", "IsBanned", "Password", "Role", "Username" },
                values: new object[] { new Guid("9e94de4c-ad25-4430-9c0c-27b1787690bd"), "admin", "sosig132@gmail.com", null, "$2a$11$lXOojQTkHQcw1BiKnTOgSOYKrLA65z4myVP.7mbltsNwLZZJTHuDG", 0, "admin" });
        }
    }
}
