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
        public static Patient SelectPatient(int patientID)
        {
            return db.Patients.First(a => a.PatientID == patientID);
        }

        public static Refill AdminSelectRefill(int refillID)
        {
            return db.Refills.First(a => a.RefillID == refillID);
        }

        #endregion

        #region Add Processing
        public static void AddPatient(string fName, string lName, DateTime birthDate)
        {
            db.Add(new Patient(fName, lName, birthDate));
            db.Add(new Transaction(ClassAction.Add, ClassesEnum.Patient, $"Added Patient {lName}, {fName} | Birthdate: {birthDate.ToString("d")}"));
            db.SaveChanges();
        }

        public static void AddPrescription(int patientID, string rxName, int refillQuantity, int pillQuantity, DateTime expireDate)
        {
            var patientIDMax = db.Prescriptions.Where(a => a.PrescriptionPatientID == patientID).Select(a => a.PrescriptionPatientTrackingNumber).Max();
            if(patientIDMax == 0)
            {
                patientIDMax++;
            }
            else
            {
                patientIDMax++;
            }

            db.Prescriptions.Add(new Prescription(patientID, patientIDMax, rxName, refillQuantity, pillQuantity, expireDate));
            db.Transactions.Add(new Transaction(ClassAction.Add, ClassesEnum.Prescription, ID_Type.Prescription, patientID, $"Added Prescription {rxName} | Patient ID: {patientID} | Quantity: {pillQuantity} | Refills: {refillQuantity} | Expire: {expireDate.ToString("d")}"));
            db.SaveChanges();
        }

        public static void AddRefill(int rxID, int patientID)
        {
            db.Add(new Refill(rxID, patientID));
            var rx = db.Prescriptions.First(a => a.PrescriptionID == rxID);
            rx.PrescriptionRefillRemaining -= 1;
            db.Add(new Transaction(ClassAction.Add, ClassesEnum.Refill, ID_Type.Patient, patientID, $"Added Refill | RX ID: {rxID} | Patient ID: {patientID} | Remaining Refills: {db.Prescriptions.First(a => a.PrescriptionID == rxID).PrescriptionRefillRemaining}"));
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

        public static IEnumerable<Prescription> ListPrescriptions()
        {
            return db.Prescriptions;
        }

        public static IEnumerable<Prescription> ListPrescriptionsByPatientID(int selectedPatientID)
        {
            return db.Prescriptions.Where(a => a.PrescriptionPatientID == selectedPatientID);
        }

        /// <summary>
        /// Returns Enumerable list of prescriptions for patient that are active and have refills remaining
        /// </summary>
        /// <param name="selectedPatientID">ID of Patient</param>
        /// <returns></returns>
        public static IEnumerable<Prescription> ListPrescriptionRefillActiveByPatientID(int selectedPatientID)
        {
            return db.Prescriptions.Where(a => a.PrescriptionPatientID == selectedPatientID).Where(a => a.PrescriptionActive == true).Where(a => a.PrescriptionRefillRemaining > 0);
        }

        public static IEnumerable<Refill> ListRefillsAll()
        {
            return db.Refills;
        }

        public static IEnumerable<Refill> ListRefillsByPatientID(int selectedPatientID)
        {
            return db.Refills.Where(a => a.RefillPatientID == selectedPatientID);
        }

        public static IEnumerable<Refill> AdminListRefillNeedFilled()
        {
            return db.Refills.Where(a => a.RefillFilled == false);
        }

        public static IEnumerable<Transaction> ListAllTransactions()
        {
            return db.Transactions;
        }

        public static IEnumerable<Transaction> ListAllTransactionsByPatientID(int selectedPatientID)
        {
            return db.Transactions.Where(a => a.TransactionPatientID == selectedPatientID);
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
            var refill = db.Refills.First(a => a.RefillID == updatedRefill.RefillID);

            refill.RefillPatientID = updatedRefill.RefillPatientID;
            refill.RefillRxID = updatedRefill.RefillRxID;

            db.SaveChanges();
        }

        public static void ModifyRefillFilled(Refill updatedRefill)
        {
            var refill = db.Refills.First(a => a.RefillID == updatedRefill.RefillID);

            refill.RefillEntryDate = DateTime.Now;
            refill.RefillFilled = true;
        }
        #endregion

        #region Activate / Delete / Disable Processing
        public static void AdminActivatePatient(Patient patient)
        {
            var patientObject = db.Patients.First(a => a.PatientID == patient.PatientID);
            patientObject.PatientActive = true;
            db.Transactions.Add(new Transaction(ClassAction.Activate, ClassesEnum.Patient, ID_Type.Patient, patient.PatientID, $"Activated Patient ID: {patient.PatientID} | Full Name: {patient.PatientFullName}"));
            db.SaveChanges();
        }

        public static void DisablePatient(Patient patient)
        {
            var patientObject = db.Patients.First(a => a.PatientID == patient.PatientID);
            patientObject.PatientActive = false;
            db.Transactions.Add(new Transaction(ClassAction.Disable, ClassesEnum.Patient, ID_Type.Patient, patient.PatientID, $"Disabled Patient ID: {patient.PatientID} | Full Name {patient.PatientFullName}"));
            db.SaveChanges();
        }

        public static void DisablePrescription(Prescription rx)
        {
            var prescriptionObject = db.Prescriptions.First(a => a.PrescriptionID == rx.PrescriptionID);
            prescriptionObject.PrescriptionActive = false;
            db.Transactions.Add(new Transaction(ClassAction.Disable, ClassesEnum.Prescription, ID_Type.Prescription, rx.PrescriptionID, $"Disabled Prescription ID: {rx.PrescriptionID} | Prescription Name: {rx.PrescriptionName}"));
            db.SaveChanges();
        }

        public static void AdminDeletePatient()
        {

        }

        public static void AdminDeletePrescription()
        {

        }

        public static void AdminDeleteRefill()
        {

        }
        #endregion



    }
}
