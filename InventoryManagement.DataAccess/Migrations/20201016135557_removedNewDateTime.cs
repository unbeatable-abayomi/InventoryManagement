using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManagement.DataAccess.Migrations
{
    public partial class removedNewDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "StockItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "StockItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
