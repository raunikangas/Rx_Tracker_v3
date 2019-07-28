using Microsoft.EntityFrameworkCore.Migrations;

namespace Rx_Tracker_v3.Migrations
{
    public partial class Test_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrescriptionPillQuanity",
                table: "Prescriptions",
                newName: "PrescriptionPillQuantityRemaining");

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionPillQuantity",
                table: "Prescriptions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrescriptionPillQuantity",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "PrescriptionPillQuantityRemaining",
                table: "Prescriptions",
                newName: "PrescriptionPillQuanity");
        }
    }
}
