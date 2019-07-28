using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rx_Tracker_v3.Migrations
{
    public partial class Test_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RefillFillDate",
                table: "Refills",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "RefillFilled",
                table: "Refills",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefillFillDate",
                table: "Refills");

            migrationBuilder.DropColumn(
                name: "RefillFilled",
                table: "Refills");
        }
    }
}
