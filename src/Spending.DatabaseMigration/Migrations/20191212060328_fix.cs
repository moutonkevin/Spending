using Microsoft.EntityFrameworkCore.Migrations;

namespace Spending.DatabaseMigration.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionCategoryPatterns_Category_CategoryId",
                table: "TransactionCategoryPatterns");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionCategoryPatterns_User_UserId",
                table: "TransactionCategoryPatterns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionCategoryPatterns",
                table: "TransactionCategoryPatterns");

            migrationBuilder.RenameTable(
                name: "TransactionCategoryPatterns",
                newName: "TransactionCategoryPattern");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionCategoryPatterns_UserId",
                table: "TransactionCategoryPattern",
                newName: "IX_TransactionCategoryPattern_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionCategoryPatterns_CategoryId",
                table: "TransactionCategoryPattern",
                newName: "IX_TransactionCategoryPattern_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionCategoryPattern",
                table: "TransactionCategoryPattern",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionCategoryPattern_Category_CategoryId",
                table: "TransactionCategoryPattern",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionCategoryPattern_User_UserId",
                table: "TransactionCategoryPattern",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionCategoryPattern_Category_CategoryId",
                table: "TransactionCategoryPattern");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionCategoryPattern_User_UserId",
                table: "TransactionCategoryPattern");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionCategoryPattern",
                table: "TransactionCategoryPattern");

            migrationBuilder.RenameTable(
                name: "TransactionCategoryPattern",
                newName: "TransactionCategoryPatterns");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionCategoryPattern_UserId",
                table: "TransactionCategoryPatterns",
                newName: "IX_TransactionCategoryPatterns_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionCategoryPattern_CategoryId",
                table: "TransactionCategoryPatterns",
                newName: "IX_TransactionCategoryPatterns_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionCategoryPatterns",
                table: "TransactionCategoryPatterns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionCategoryPatterns_Category_CategoryId",
                table: "TransactionCategoryPatterns",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionCategoryPatterns_User_UserId",
                table: "TransactionCategoryPatterns",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
