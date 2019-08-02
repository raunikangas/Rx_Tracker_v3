using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rx_Tracker_v3.Migrations
{
    public partial class Test_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PrescriptionNextRefillEnableDate",
                table: "Prescriptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "PatientEmail",
                table: "Patients",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrescriptionNextRefillEnableDate",
                table: "Prescriptions");

            migrationBuilder.AlterColumn<string>(
                name: "PatientEmail",
                table: "Patients",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
