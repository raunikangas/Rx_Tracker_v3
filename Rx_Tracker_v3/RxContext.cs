using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Rx_Tracker_v3
{
    public class RxContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Refill> Refills { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog=RxTracking_v3_DB; Integrated Security = True; Connect Timeout = 30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<Patient>().HasKey(p => p.PatientID);
            modelBuilder.Entity<Patient>(p =>
            {
                p.Property(a => a.PatientID)
                .ValueGeneratedOnAdd();

                p.Property(a => a.PatientFirstName).IsRequired();
                p.Property(a => a.PatientLastName).IsRequired();
                p.Property(a => a.PatientFullName).IsRequired();
                p.Property(a => a.PatientBirthDate).IsRequired();
                p.Property(a => a.PatientEnteredDate).IsRequired();
                p.Property(a => a.PatientEmail).IsRequired();
            });

            modelBuilder.Entity<Prescription>().ToTable("Prescriptions");
            modelBuilder.Entity<Prescription>().HasKey(rx => rx.PrescriptionID);
            modelBuilder.Entity<Prescription>(rx =>
            {
                rx.Property(a => a.PrescriptionID)
                .ValueGeneratedOnAdd();

                rx.Property(a => a.PrescriptionName).IsRequired();
                rx.Property(a => a.PrescriptionPatientID).IsRequired();
                rx.Property(a => a.PrescriptionPillQuantity).IsRequired();
                rx.Property(a => a.PrescriptionRefillQuantity).IsRequired();
                rx.Property(a => a.PrescriptionNextRefillEnableDate).IsRequired();

                rx.HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PrescriptionPatientID);
            });

            modelBuilder.Entity<Refill>().ToTable("Refills");
            modelBuilder.Entity<Refill>().HasKey(r => r.RefillID);
            modelBuilder.Entity<Refill>(refill => 
            {
                refill.Property(a => a.RefillID).ValueGeneratedOnAdd();

                refill.HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.RefillPatientID);

                //refill.HasOne(a => a.Prescription)
                //.WithMany()
                //.HasForeignKey(a => a.RefillRxID);
            });

            modelBuilder.Entity<Transaction>().ToTable("Transactions");
            modelBuilder.Entity<Transaction>().HasKey(t => t.TransactionID);
            modelBuilder.Entity<Transaction>(trans => 
            {
                trans.Property(a => a.TransactionID).ValueGeneratedOnAdd();
                trans.Property(a => a.TransactionClass).IsRequired();
                trans.Property(a => a.TransactionClassAction).IsRequired();
                trans.Property(a => a.TransactionAction).IsRequired();

            });

        }


    }
}
