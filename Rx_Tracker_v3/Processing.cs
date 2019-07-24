using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Rx_Tracker_v3
{
    public static class Processing
    {
        public static RxContext db = new RxContext();

        #region Select Actions
        public static List<Patient> SelectPatient(int patientID)
        {
            return db.Patients.Where(a => a.PatientID == patientID).ToList();
        }

        

        #endregion

        #region Add Processing
        public static void AddPatient(string fName, string lName, DateTime birthDate)
        {
            db.Add(new Patient(fName, lName, birthDate));
            db.Add(new Transaction(ClassAction.Add, ClassesEnum.Patient, $"Added Patient {lName}, {fName} | Birthdate: {birthDate.ToString("d")}"));
            db.SaveChanges();
        }

        public static void AddPrescription(int patientID, string rxName, int refillQuantity, int pillQuantity)
        {
            db.Prescriptions.Add(new Prescription(patientID, rxName, refillQuantity, pillQuantity));
            db.Transactions.Add(new Transaction(ClassAction.Add, ClassesEnum.Prescription, ID_Type.Prescription, patientID, $"Added Prescription {rxName} | Patient ID: {patientID} | Quantity: {pillQuantity} | Refills: {refillQuantity}"));
            db.SaveChanges();
        }

        public static void AddRefill(int rxID, int patientID)
        {
            db.Add(new Refill(rxID, patientID));
            db.Add(new Transaction(ClassAction.Add, ClassesEnum.Refill, ID_Type.Patient, patientID, $"Added Refill | RX ID: {rxID} | Patient ID: {patientID} | Remaining Refills: {null} ~Need to Implement~"));
            db.SaveChanges();
        }
        
        #endregion

        #region List Processing
        public static IEnumerable<Patient> ListAllPatients()
        {
            return (db.Patients.Where(a => a.PatientActive == true));
        }

        public static IEnumerable<Patient> ListIndividualPatient(int patientID)
        {
            return (db.Patients.Where(a => a.PatientID == patientID));
        }

        public static void ListPrescriptions()
        {

        }

        public static IEnumerable<Prescription> ListPrescriptionsByPatientID(int selectedPatientID)
        {
            return db.Prescriptions.Where(a => a.PrescriptionPatientID == selectedPatientID);
        }

        public static IEnumerable<Refill> ListRefillsByPatientID(int selectedPatientID)
        {
            return db.Refills.Where(a => a.RefillPatientID == selectedPatientID);
        }

        public static IEnumerable<Transaction> ListAllTransactions()
        {
            return db.Transactions;
        }

        public static void ListAllTransactionsByPatientID()
        {

        }
        #endregion

        #region Modify Processing
        public static void ModifyPatient(Patient updatedPatientData)
        {
            var patient = db.Patients.First<Patient>(a => a.PatientID == updatedPatientData.PatientID);
            patient.PatientFullName = updatedPatientData.PatientFullName;
            patient.PatientFirstName = updatedPatientData.PatientFirstName;
            patient.PatientLastName = updatedPatientData.PatientLastName;
            patient.PatientBirthDate = updatedPatientData.PatientBirthDate;

            db.SaveChanges();
        }

        public static void ModifyPrescription(Prescription updatedPrescription)
        {
            var rx = db.Prescriptions.First(a => a.PrescriptionPatientID == updatedPrescription.PrescriptionPatientID);
            rx.PrescriptionName = updatedPrescription.PrescriptionName;
            rx.PrescriptionPillDose = updatedPrescription.PrescriptionPillDose;
            rx.PrescriptionRefillQuantity = updatedPrescription.PrescriptionRefillQuantity;
            rx.PrescriptionRefillRemaining = updatedPrescription.PrescriptionRefillRemaining;

            db.SaveChanges();
        }

        public static void ModifyRefill(Refill updatedRefill)
        {

        }
        #endregion

        #region Delete / Disable Processing
        public static void DisablePatient()
        {

        }

        public static void DisablePrescription()
        {

        }

        public static void DisableRefill()
        {

        }

        #endregion



    }
}
