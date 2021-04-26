namespace BankSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddBalanceColumnInBankAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                "Balance",
                "Accounts",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                "Currency",
                "Accounts",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                "TypeId",
                "Accounts",
                nullable: false);

            migrationBuilder.AddColumn<decimal>(
                "Credit",
                "Accounts",
                nullable: false);

            migrationBuilder.AddColumn<float>(
                "InterestRate",
                "Accounts",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Balance",
                "Accounts");

            migrationBuilder.DropColumn(
                "Currency",
                "Accounts");

            migrationBuilder.DropColumn(
                "TypeId",
                "Accounts");

            migrationBuilder.DropColumn(
                "Credit",
                "Accounts");

            migrationBuilder.DropColumn(
                "InterestRate",
                "Accounts");
        }
    }
}