using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManagement.DataAccess.Migrations
{
    public partial class addedNewDateString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateCreated",
                table: "StockItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "StockItems");
        }
    }
}
