using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaAPI.Migrations
{
    /// <inheritdoc />
    public partial class removeCategeryFromProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Procedures");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Procedures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
