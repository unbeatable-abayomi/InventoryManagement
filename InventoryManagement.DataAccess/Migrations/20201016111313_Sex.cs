using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManagement.DataAccess.Migrations
{
    public partial class Sex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Employees");
        }
    }
}
