using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account_Menagment_System.Server.Migrations
{
    /// <inheritdoc />
    public partial class Expirations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Account",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Account",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Account");
        }
    }
}
