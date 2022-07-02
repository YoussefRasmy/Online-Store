using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStoreBack_API.Migrations
{
    public partial class HandlToMatchFront : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "CartProducts");

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orders");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "CartProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
