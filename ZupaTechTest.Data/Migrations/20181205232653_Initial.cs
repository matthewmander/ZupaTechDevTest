using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZupaTechTest.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeetingSlots",
                columns: table => new
                {
                    MeetingSlotId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingSlots", x => x.MeetingSlotId);
                });

            migrationBuilder.CreateTable(
                name: "SeatBookings",
                columns: table => new
                {
                    SeatBookingId = table.Column<Guid>(nullable: false),
                    SeatNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    MeetingSlotId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatBookings", x => x.SeatBookingId);
                    table.ForeignKey(
                        name: "FK_SeatBookings_MeetingSlots_MeetingSlotId",
                        column: x => x.MeetingSlotId,
                        principalTable: "MeetingSlots",
                        principalColumn: "MeetingSlotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeatBookings_MeetingSlotId",
                table: "SeatBookings",
                column: "MeetingSlotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatBookings");

            migrationBuilder.DropTable(
                name: "MeetingSlots");
        }
    }
}
