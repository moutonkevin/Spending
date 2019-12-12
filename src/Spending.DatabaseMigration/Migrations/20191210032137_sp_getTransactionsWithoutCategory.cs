using Microsoft.EntityFrameworkCore.Migrations;

namespace Spending.DatabaseMigration.Migrations
{
    public partial class sp_getTransactionsWithoutCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
CREATE PROCEDURE sp_getTransactionsWithoutCategory
	@userId INT
AS
BEGIN
	                        
	SELECT t.[Id]
      ,t.[Amount]
      ,t.[Description]
      ,t.[Date]
      ,t.[AccountId]
      ,t.[TransactionTypeId]
      ,t.[UserId]
	FROM dbo.[Transaction] t
	WHERE t.UserId = @userId	
	AND Id NOT IN 
	(
		SELECT t.Id
		FROM dbo.[Transaction] t, dbo.TransactionCategoryPattern tduc
		WHERE t.UserId = @userId
		AND t.[Description]  LIKE '%' + tduc.Pattern + '%'	
	)
END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
