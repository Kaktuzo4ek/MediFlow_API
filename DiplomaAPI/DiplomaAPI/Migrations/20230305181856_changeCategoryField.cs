using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaAPI.Migrations
{
    /// <inheritdoc />
    public partial class changeCategoryField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Referrals");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Referrals",
                type: "int",
                nullable: true,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "ReferralCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Referrals_CategoryId",
                table: "Referrals",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Referrals_ReferralCategories_CategoryId",
                table: "Referrals",
                column: "CategoryId",
                principalTable: "ReferralCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Referrals_ReferralCategories_CategoryId",
                table: "Referrals");

            migrationBuilder.DropTable(
                name: "ReferralCategories");

            migrationBuilder.DropIndex(
                name: "IX_Referrals_CategoryId",
                table: "Referrals");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Referrals");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Referrals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
