using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStoreBack_API.Migrations
{
    public partial class UpdateTheUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomersMobile",
                columns: table => new
                {
                    MobileNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersMobile", x => x.MobileNumber);
                    table.ForeignKey(
                        name: "FK_CustomersMobile_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomersMobile_CustomerId",
                table: "CustomersMobile",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomersMobile");
        }
    }
}
