using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End.Migrations
{
    /// <inheritdoc />
    public partial class dsadsa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3dacede6-b48f-45e7-afdb-2afbe89251b4"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DisplayName", "Email", "IsBanned", "Password", "Role", "Username" },
                values: new object[] { new Guid("7b31b0f1-3709-44cc-a792-83c1187b09e1"), "admin", "sosig132@gmail.com", null, "$2a$11$girlM7gSSdwgzkP0nHvI1uaLmG09sdr759FxbvLZ4mckAaaKjASny", 0, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7b31b0f1-3709-44cc-a792-83c1187b09e1"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DisplayName", "Email", "IsBanned", "Password", "Role", "Username" },
                values: new object[] { new Guid("3dacede6-b48f-45e7-afdb-2afbe89251b4"), "admin", "sosig132@gmail.com", null, "$2a$11$WPRSmnQzAkwXdLgr9fcgMek.YGNfWmoQA0UYnJDDQKdN.ofg2B/vO", 0, "admin" });
        }
    }
}
