using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account_Menagment_System.Server.Migrations
{
    /// <inheritdoc />
    public partial class BotExpiration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBotActive",
                table: "Account",
                newName: "BotState");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BotState",
                table: "Account",
                newName: "IsBotActive");
        }
    }
}
