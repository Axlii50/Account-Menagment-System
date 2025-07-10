using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account_Menagment_System.Server.Migrations
{
    /// <inheritdoc />
    public partial class MacAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BotMacAddress",
                table: "Account",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "SessionsExpirationOveride",
                table: "Account",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BotMacAddress",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "SessionsExpirationOveride",
                table: "Account");
        }
    }
}
