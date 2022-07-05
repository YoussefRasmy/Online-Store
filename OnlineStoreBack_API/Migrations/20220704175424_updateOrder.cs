using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStoreBack_API.Migrations
{
    public partial class updateOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderState",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "shipping_Price",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderState",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "shipping_Price",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
