using Microsoft.EntityFrameworkCore.Migrations;

namespace Spending.DatabaseMigration.Migrations
{
    public partial class Pattern : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionDescriptionUserCategory");

            migrationBuilder.CreateTable(
                name: "TransactionCategoryPatterns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pattern = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCategoryPatterns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionCategoryPatterns_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionCategoryPatterns_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategoryPatterns_CategoryId",
                table: "TransactionCategoryPatterns",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategoryPatterns_UserId",
                table: "TransactionCategoryPatterns",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionCategoryPatterns");

            migrationBuilder.CreateTable(
                name: "TransactionDescriptionUserCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDescriptionUserCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionDescriptionUserCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionDescriptionUserCategory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDescriptionUserCategory_CategoryId",
                table: "TransactionDescriptionUserCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDescriptionUserCategory_UserId",
                table: "TransactionDescriptionUserCategory",
                column: "UserId");
        }
    }
}
