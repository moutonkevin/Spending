using Microsoft.EntityFrameworkCore.Migrations;

namespace Spending.DatabaseMigration.Migrations
{
    public partial class eatingout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TransactionCategory_TransactionId",
                table: "TransactionCategory");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Eating out");

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[] { 16, "Haircut" });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategory_TransactionId",
                table: "TransactionCategory",
                column: "TransactionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TransactionCategory_TransactionId",
                table: "TransactionCategory");

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Lunch");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategory_TransactionId",
                table: "TransactionCategory",
                column: "TransactionId");
        }
    }
}
