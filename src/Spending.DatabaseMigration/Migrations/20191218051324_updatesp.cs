using Microsoft.EntityFrameworkCore.Migrations;

namespace Spending.DatabaseMigration.Migrations
{
    public partial class updatesp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
ALTER PROCEDURE [dbo].[sp_getTransactionsSatisfyingCategoryPattern]
	@userId INT,
	@transactionCategoryPatternId INT
AS
BEGIN
	                        
		SELECT t.[Id]
		  ,t.[Amount]
		  ,t.[Description]
		  ,t.[Date]
		  ,t.[AccountId]
		  ,t.[TransactionTypeId]
		  ,t.[UserId]
		FROM dbo.TransactionCategoryPattern tduc,
			dbo.[Transaction] t
		LEFT JOIN dbo.TransactionCategory tc
			ON t.Id = tc.TransactionId
		WHERE t.UserId = @userId
			AND tduc.UserId	= @userId
			AND tduc.Id = @transactionCategoryPatternId
			AND t.Description  LIKE '%' + tduc.Pattern + '%'	
			AND tc.TransactionId IS NULL

END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
