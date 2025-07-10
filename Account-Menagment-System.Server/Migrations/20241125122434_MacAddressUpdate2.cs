using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account_Menagment_System.Server.Migrations
{
    /// <inheritdoc />
    public partial class MacAddressUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SessionsExpirationOveride",
                table: "Account",
                type: "decimal(38,10)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SessionsExpirationOveride",
                table: "Account",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,10)");
        }
    }
}
