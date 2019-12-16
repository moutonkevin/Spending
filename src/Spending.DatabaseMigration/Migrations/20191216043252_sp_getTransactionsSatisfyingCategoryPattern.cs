using Microsoft.EntityFrameworkCore.Migrations;

namespace Spending.DatabaseMigration.Migrations
{
    public partial class sp_getTransactionsSatisfyingCategoryPattern : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[] { 15, "Lunch" });

            var sp = @"
CREATE PROCEDURE [dbo].[sp_getTransactionsSatisfyingCategoryPattern]
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
		FROM dbo.[Transaction] t, dbo.TransactionCategoryPattern tduc
		WHERE t.UserId = @userId
			AND tduc.UserId	= @userId
			AND tduc.Id = @transactionCategoryPatternId
			AND t.Description  LIKE '%' + tduc.Pattern + '%'	

END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
