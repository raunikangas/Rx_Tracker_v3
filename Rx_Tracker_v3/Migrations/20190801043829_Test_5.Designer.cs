﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rx_Tracker_v3;

namespace Rx_Tracker_v3.Migrations
{
    [DbContext(typeof(RxContext))]
    [Migration("20190801043829_Test_5")]
    partial class Test_5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Rx_Tracker_v3.Patient", b =>
                {
                    b.Property<int>("PatientID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("PatientActive");

                    b.Property<DateTime>("PatientBirthDate");

                    b.Property<string>("PatientEmail")
                        .IsRequired();

                    b.Property<DateTime>("PatientEnteredDate");

                    b.Property<string>("PatientFirstName")
                        .IsRequired();

                    b.Property<string>("PatientFullName")
                        .IsRequired();

                    b.Property<string>("PatientLastName")
                        .IsRequired();

                    b.Property<string>("PatientPhoneNumber");

                    b.HasKey("PatientID");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Rx_Tracker_v3.Prescription", b =>
                {
                    b.Property<int>("PrescriptionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("PrescriptionActive");

                    b.Property<DateTime>("PrescriptionEntryDate");

                    b.Property<DateTime>("PrescriptionExpireDate");

                    b.Property<string>("PrescriptionName")
                        .IsRequired();

                    b.Property<DateTime>("PrescriptionNextRefillEnableDate");

                    b.Property<int>("PrescriptionPatientID");

                    b.Property<int>("PrescriptionPatientTrackingNumber");

                    b.Property<int>("PrescriptionPillDose");

                    b.Property<int>("PrescriptionPillQuantity");

                    b.Property<int>("PrescriptionPillQuantityRemaining");

                    b.Property<int>("PrescriptionRefillQuantity");

                    b.Property<int>("PrescriptionRefillRemaining");

                    b.HasKey("PrescriptionID");

                    b.HasIndex("PrescriptionPatientID");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("Rx_Tracker_v3.Refill", b =>
                {
                    b.Property<int>("RefillID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PrescriptionID");

                    b.Property<DateTime>("RefillEntryDate");

                    b.Property<DateTime>("RefillFillDate");

                    b.Property<bool>("RefillFilled");

                    b.Property<int>("RefillPatientID");

                    b.Property<int>("RefillRxID");

                    b.HasKey("RefillID");

                    b.HasIndex("PrescriptionID");

                    b.HasIndex("RefillPatientID");

                    b.ToTable("Refills");
                });

            modelBuilder.Entity("Rx_Tracker_v3.Transaction", b =>
                {
                    b.Property<int>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TransactionAction")
                        .IsRequired();

                    b.Property<int>("TransactionClass");

                    b.Property<int>("TransactionClassAction");

                    b.Property<DateTime>("TransactionEntryDateTime");

                    b.Property<int>("TransactionIDType");

                    b.Property<int>("TransactionPatientID");

                    b.HasKey("TransactionID");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Rx_Tracker_v3.Prescription", b =>
                {
                    b.HasOne("Rx_Tracker_v3.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PrescriptionPatientID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Rx_Tracker_v3.Refill", b =>
                {
                    b.HasOne("Rx_Tracker_v3.Prescription", "Prescription")
                        .WithMany()
                        .HasForeignKey("PrescriptionID");

                    b.HasOne("Rx_Tracker_v3.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("RefillPatientID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
