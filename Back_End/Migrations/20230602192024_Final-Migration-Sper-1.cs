using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End.Migrations
{
    /// <inheritdoc />
    public partial class FinalMigrationSper1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DisplayName", "Email", "IsBanned", "Password", "Role" },
                values: new object[] { new Guid("8377d282-e04d-4eb6-978e-1831f632aad0"), "admin", "sosig132@gmail.com", null, "$2a$11$i9ubi9t1oKSp3Y51PN8hR.FSVrLcv9boC7Pt8DuAxTpeFLWzY.N82", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8377d282-e04d-4eb6-978e-1831f632aad0"));
        }
    }
}
