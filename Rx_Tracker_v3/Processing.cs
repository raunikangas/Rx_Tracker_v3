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
        public static IEnumerable<Patient> ListPatient()
        {
            return (db.Patients);
        }

        public static IEnumerable<Patient> ListIndividualPatient(int patientID)
        {
            return (db.Patients.Where(a => a.PatientID == patientID));
        }

        public static void ListPrescriptions()
        {

        }

        public static IEnumerable<Prescription> ListPrescriptions(int selectedPatientID)
        {
            return db.Prescriptions.Where(a => a.PrescriptionPatientID == selectedPatientID);
        }

        public static void ListRefills()
        {

        }

        public static void ListTransactions()
        {

        }
        #endregion

        #region Modify Processing
        public static void ModifyPatient(Patient updatedPatientData)
        {

        }

        public static void ModifyPrescription()
        {

        }

        public static void ModifyRefill()
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
