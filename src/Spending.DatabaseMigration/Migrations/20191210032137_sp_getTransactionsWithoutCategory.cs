using Microsoft.EntityFrameworkCore.Migrations;

namespace Spending.DatabaseMigration.Migrations
{
    public partial class sp_getTransactionsWithoutCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE sp_getTransactionsWithoutCategory
	                        @userId INT
                        AS
                        BEGIN
	                        
	                        SELECT t.*
	                        FROM dbo.[Transaction] t, dbo.TransactionDescriptionUserCategory tduc
	                        WHERE t.UserId = @userId
		                        AND t.[Description] LIKE '%' + tduc.Description + '%'	
                        END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
