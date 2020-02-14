using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTracker.Migrations
{
    public partial class AddFilterExpensesStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(CreateStoredProcedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(RemoveStoredProcedure);
        }

        private const string CreateStoredProcedure = @"
            CREATE PROCEDURE [dbo].[spFilterExpenses]
	            @userId nvarchar(255),
	            @dateFrom datetime,
	            @dateTo datetime,
	            @categoryId int,
	            @clientId int
            AS
            BEGIN
	            SET NOCOUNT ON;

	            SELECT *
	            FROM Expenses
	            WHERE 
		            UserId = @userId AND 
		            ((@clientId IS NULL) OR (ClientId = @clientId)) AND 
		            ((@categoryId IS NULL) OR (CategoryId = @categoryId)) AND 
		            Date BETWEEN @dateFrom AND @dateTo
            END
            GO";

        private const string RemoveStoredProcedure = @"
            DROP PROCEDURE [dbo].[spFilterExpenses]";
    }
}
