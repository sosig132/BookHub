using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End.Migrations
{
    /// <inheritdoc />
    public partial class ASd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1f6bd821-1c53-4482-adbe-116bd5084818"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DisplayName", "Email", "IsBanned", "Password", "Role", "Username" },
                values: new object[] { new Guid("3dacede6-b48f-45e7-afdb-2afbe89251b4"), "admin", "sosig132@gmail.com", null, "$2a$11$WPRSmnQzAkwXdLgr9fcgMek.YGNfWmoQA0UYnJDDQKdN.ofg2B/vO", 0, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3dacede6-b48f-45e7-afdb-2afbe89251b4"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DisplayName", "Email", "IsBanned", "Password", "Role", "Username" },
                values: new object[] { new Guid("1f6bd821-1c53-4482-adbe-116bd5084818"), "admin", "sosig132@gmail.com", null, "$2a$11$pz5xjBC5K2y/FXn2MSf5SuM0W24K2fpzHngq9ZUTm12m/CpczWmyS", 0, "admin" });
        }
    }
}
