using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTracker.Migrations
{
    public partial class AddHoursAndRemoveNameFromExpenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Expenses");

            migrationBuilder.AddColumn<decimal>(
                name: "Hours",
                table: "Expenses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hours",
                table: "Expenses");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
