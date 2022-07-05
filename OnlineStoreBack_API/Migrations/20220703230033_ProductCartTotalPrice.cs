using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStoreBack_API.Migrations
{
    public partial class ProductCartTotalPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "CartProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "CartProducts");
        }
    }
}
