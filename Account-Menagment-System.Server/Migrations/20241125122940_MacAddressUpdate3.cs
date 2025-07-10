using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account_Menagment_System.Server.Migrations
{
    /// <inheritdoc />
    public partial class MacAddressUpdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SessionsExpirationOveride",
                table: "Account",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,10)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SessionsExpirationOveride",
                table: "Account",
                type: "decimal(38,10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
