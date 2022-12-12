using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flight_eBooking.Migrations
{
    public partial class DescDest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescDest",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescDest",
                table: "Destinations");
        }
    }
}
