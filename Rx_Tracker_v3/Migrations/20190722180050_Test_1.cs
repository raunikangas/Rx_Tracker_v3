using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rx_Tracker_v3.Migrations
{
    public partial class Test_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientFirstName = table.Column<string>(nullable: false),
                    PatientLastName = table.Column<string>(nullable: false),
                    PatientFullName = table.Column<string>(nullable: false),
                    PatientBirthDate = table.Column<DateTime>(nullable: false),
                    PatientEnteredDate = table.Column<DateTime>(nullable: false),
                    PatientEmail = table.Column<string>(nullable: true),
                    PatientPhoneNumber = table.Column<string>(nullable: true),
                    PatientActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientID);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TransactionAction = table.Column<string>(nullable: true),
                    TransactionClass = table.Column<int>(nullable: false),
                    TransactionClassAction = table.Column<int>(nullable: false),
                    TransactionEntryDateTime = table.Column<DateTime>(nullable: false),
                    TransactionPatientID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PrescriptionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PrescriptionPatientID = table.Column<int>(nullable: false),
                    PrescriptionName = table.Column<string>(nullable: false),
                    PrescriptionPatientTrackingNumber = table.Column<int>(nullable: false),
                    PrescriptionRefillQuantity = table.Column<int>(nullable: false),
                    PrescriptionRefillRemaining = table.Column<int>(nullable: false),
                    PrescriptionPillQuanity = table.Column<int>(nullable: false),
                    PrescriptionPillDose = table.Column<int>(nullable: false),
                    PrescriptionExpireDate = table.Column<DateTime>(nullable: false),
                    PrescriptionEntryDate = table.Column<DateTime>(nullable: false),
                    PrescriptionActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.PrescriptionID);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_PrescriptionPatientID",
                        column: x => x.PrescriptionPatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Refills",
                columns: table => new
                {
                    RefillID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RefillRxID = table.Column<int>(nullable: false),
                    RefillPatientID = table.Column<int>(nullable: false),
                    RefillEntryDate = table.Column<DateTime>(nullable: false),
                    PrescriptionID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refills", x => x.RefillID);
                    table.ForeignKey(
                        name: "FK_Refills_Prescriptions_PrescriptionID",
                        column: x => x.PrescriptionID,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Refills_Patients_RefillPatientID",
                        column: x => x.RefillPatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PrescriptionPatientID",
                table: "Prescriptions",
                column: "PrescriptionPatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Refills_PrescriptionID",
                table: "Refills",
                column: "PrescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Refills_RefillPatientID",
                table: "Refills",
                column: "RefillPatientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Refills");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
