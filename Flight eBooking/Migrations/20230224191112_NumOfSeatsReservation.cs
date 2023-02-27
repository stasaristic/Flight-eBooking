using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flight_eBooking.Migrations
{
    public partial class NumOfSeatsReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfSeats",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfSeats",
                table: "Reservations");
        }
    }
}
