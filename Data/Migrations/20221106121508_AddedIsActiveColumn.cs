using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductCatalogue.Data.Migrations
{
    public partial class AddedIsActiveColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "SubCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Categories");
        }
    }
}
