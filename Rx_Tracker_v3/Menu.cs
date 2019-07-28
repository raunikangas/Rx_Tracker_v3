using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Rx_Tracker_v3
{
    public static class Menu
    {

        #region Console Processing
        public static int ConsoleIntProcessing(string consoleOutput)
        {
            while(true)
            {
                Console.Write(consoleOutput);
                try
                {
                    return Convert.ToInt32(Console.ReadLine());
                }
                catch(FormatException)
                {
                    Console.WriteLine("[!] Error Processing Console Input to Int");
                }
            }
        }
        
        public static void ListPatientOutput(IEnumerable<Patient> patientsList)
        {
            //foreach (var patient in patientsList)
            //{
            //    Console.WriteLine($"Patient ID: {patient.PatientID} | Patient Name: {patient.PatientFullName} | Patient Birthdate: {patient.PatientBirthDate.ToString("d")}");
            //}
            

            Console.WriteLine($"ID | Patient Name | Birth Date | Active Rx Count");
            foreach (var patient in patientsList)
            {
                var rxCount = Processing.db.Prescriptions.Where(a => a.PrescriptionPatientID == patient.PatientID).Where(a => a.PrescriptionActive == true).Count();
                Console.WriteLine($"{patient.PatientID} | {patient.PatientFullName} | {patient.PatientBirthDate.ToString("d")} | {rxCount}");
            }
        }

        public static void ListPrescriptionOutput(IEnumerable<Prescription> prescriptionList)
        {
            Console.WriteLine($"ID | Name | Quantity | Rem Refill | Expire Date");
            foreach(var rx in prescriptionList)
            {
                Console.WriteLine($"{rx.PrescriptionID} | {rx.PrescriptionName} | {rx.PrescriptionPillQuantity} | {rx.PrescriptionRefillRemaining} | {rx.PrescriptionExpireDate.ToString("d")}");
            }
        }

        public static void ListRefillOutput(IEnumerable<Refill> refillList)
        {
            Console.WriteLine($"ID | RX Name | Filled | Submitted Date | Filled Date");
            foreach(var refill in refillList)
            {
                Console.WriteLine($"{refill.RefillID} | {Processing.db.Prescriptions.Where(a => a.PrescriptionID == refill.RefillRxID).Select(a => a.PrescriptionName)} | {refill.RefillFilled} | {refill.RefillEntryDate.ToString("d")} | {refill.RefillFillDate.ToString("d")}");
            }
        }

        public static DateTime ConsoleDateProcessing(string consoleOutput, string outputType)
        {
            while(true)
            {
                try
                {
                    Console.WriteLine($"{consoleOutput}");
                    int month = ConsoleIntProcessing($"\t\t{outputType} Month (Ex. 01): ");
                    int day = ConsoleIntProcessing($"\t\t{outputType} Day (Ex. 01): ");
                    int year = ConsoleIntProcessing($"\t\t{outputType} Year (Ex. 1980): ");
                    return Convert.ToDateTime($"{month}/{day}/{year}");
                }
                catch(FormatException)
                {
                    Console.WriteLine("[!] Error Processing Date Entry - Please Try Again!");
                }
            }
        }

        public static char ConsoleYesNoProcessing(string consoleOutput)
        {
            while(true)
            {
                Console.WriteLine($"{consoleOutput}");
                try
                {
                    char input = Convert.ToChar(Console.ReadLine());
                    if(input.ToString().ToLower() == "y" || input.ToString().ToLower() == "n")
                    {
                        return input;
                    }
                    Console.WriteLine("[!] Please enter Y or N");
                }
                catch(FormatException)
                {
                    Console.WriteLine("[!] Could Not Process Input Value - Please Try Again!");
                }
            }
        }
        #endregion


        public static void MainMenu()
        {
            while(true)
            {
                Console.WriteLine("~ Main Menu Options ~");
                Console.WriteLine("\t0 - Exit Application");
                Console.WriteLine("\t1 - Add Patient");
                Console.WriteLine("\t2 - List Patient");
                Console.WriteLine("\t3 - Select Patients");
                Console.WriteLine("\t4 - ");
                Console.WriteLine("\t10 ~ Patient Admin Menu ~");

                switch (ConsoleIntProcessing("Enter Menu Option: "))
                {
                    case 0:
                        return;
                    case 1:
                        try
                        {
                            //TODO: Menu - Add Patient with brithdate
                            Console.WriteLine("~ Add Patient ~");
                            Console.Write("\tPatient First Name: ");
                            string firstName = Console.ReadLine();
                            Console.Write("\tPatient Last Name: ");
                            string lastName = Console.ReadLine();
                            DateTime birthDate = ConsoleDateProcessing("\tPatient Birth Date: ", "Birth");

                            Processing.AddPatient(firstName, lastName, birthDate);
                            Console.WriteLine("Patient Added to Database");
                        }
                        catch(FormatException)
                        {

                        }
                        break;
                    case 2:
                        //DONE: Menu - List Patients
                        Console.WriteLine();
                        ListPatientOutput(Processing.ListAllPatients());
                        break;
                    case 3:
                        //TODO: Menu - Select Patients
                        if(Processing.db.Patients.Count() > 0)
                        {
                            Console.WriteLine();
                            ListPatientOutput(Processing.ListAllPatients());
                        }

                        Patient selectedPatient = Processing.SelectPatient(ConsoleIntProcessing("Select Patient ID: "));

                        PatientMenu(selectedPatient);
                        break;
                    case 4:
                        //TODO: Menu - 
                        break;
                    case 10:
                        //TODO: Menu - Admin Menu
                        AdminMenu();
                        break;
                }

                Console.WriteLine();
            }
            
        }

        public static void AdminMenu()
        {
            while(true)
            {
                Console.WriteLine("~ Admin Menu Options ~");
                Console.WriteLine("\t\tAdmin listing also displays disabled entries");
                Console.WriteLine("\t0 - Exit Admin Menu");
                Console.WriteLine("\t1 - List All Transactions");
                Console.WriteLine("\t2 - List All Patients");
                Console.WriteLine("\t3 - List ALl Prescriptions");
                Console.WriteLine("\t4 - List All Refills");
                Console.WriteLine("\t5 - Fill Patient Refill");
                Console.WriteLine("\t6 - Disable Patient");
                Console.WriteLine("\t7 - Delete Patient");
                Console.WriteLine("\t8 - Disable Prescription");
                Console.WriteLine("\t9 - Delete Prescription");
                Console.WriteLine("\t10 - Delete Refill");

                switch(ConsoleIntProcessing("Enter Menu Option: "))
                {
                    case 0:
                        return;
                    case 1:
                        //TODO: Admin Menu - Create List All Transaction
                        var transaction = Processing.ListAllTransactions();
                        if(transaction.Count() > 0)
                        {
                            Console.WriteLine($"ID | Patient ID | Logged Action | Log Date Time");
                            foreach (var trans in transaction)
                            {
                                Console.WriteLine($"{trans.TransactionID} | {trans.TransactionPatientID} | {trans.TransactionAction} | {trans.TransactionEntryDateTime}");
                            }
                        }
                        break;
                    case 2:
                        //DONE: Admin Menu - Create List All Patients
                        var patientsList = Processing.ListAllPatients();
                        if (patientsList.Count() > 0)
                        {
                            ListPatientOutput(patientsList);
                        }
                        else
                        {
                            Console.WriteLine("[!] No Patients to List");
                        }

                        break;
                    case 3:
                        //DONE: Admin Menu - Create List All Precriptions
                        var rxList = Processing.ListPrescriptions();
                        if (rxList.Count() > 0)
                        {
                            Console.WriteLine("\nListing All Entered Prescriptions");
                            ListPrescriptionOutput(rxList);
                        }
                        else
                        {
                            Console.WriteLine("[!] No Prescriptions to List");
                        }
                        break;
                    case 4:
                        //DONE: Admin Menu - Create List All Refills
                        var refillList = Processing.ListRefillsAll();
                        if(refillList.Count() > 0)
                        {
                            Console.WriteLine("\nListing All Entered Refills");
                        }
                        else
                        {
                            Console.WriteLine("[!] No Refills to List");
                        }
                        
                        break;
                    case 5:
                        //HIGH: Admin Menu - Create Fill Patient Refill
                        //List refills with filled = false
                        //Select refill based on ID
                        //Set filled to true
                        //Decrement prescription refillRemaining countm

                        var adminRefillList = Processing.AdminListRefillNeedFilled();
                        if(adminRefillList.Count() > 0)
                        {
                            Console.WriteLine("ID | Patient ID | RX ID | Refill Entry Date");
                            foreach(var item in adminRefillList)
                            {
                                Console.WriteLine($"{item.RefillID} | {item.RefillPatientID} | {item.RefillRxID} | {item.RefillEntryDate.ToString("d")}");
                            }

                            int selection = ConsoleIntProcessing("Enter Refill ID: ");

                        }
                        else
                        {
                            Console.WriteLine("[!] No Refills to Fill");
                        }

                        break;
                    case 6:
                        //TODO: Admin Menu - Create Disable Patient
                        break;
                    case 7:
                        //TODO: Admin Menu - Create Delete Patient
                        break;
                    case 8:
                        //TODO: Admin Menu - Create Disable Precription
                        break;
                    case 9:
                        //TODO: Admin Menu - Create Delete Prescription
                        break;
                    case 10:
                        //TODO: Admin Menu - Create Delete Refill
                        break;
                }
            }
        }

        public static void PatientMenu(Patient selectedPatient)
        {
            while (true)
            {
                Console.WriteLine($"Patient Menu Options for {selectedPatient.PatientFullName}");
                Console.WriteLine("\t0 - Exit Patient Menu");
                Console.WriteLine("\t1 - Add Prescriptions");
                Console.WriteLine("\t2 - List Prescription");
                Console.WriteLine("\t3 - Select Prescription");
                Console.WriteLine("\t4 - Modify Patient Information");
                Console.WriteLine("\t5 - Take Prescription Dose");
                Console.WriteLine("\t8 - Disable Patient");
                Console.WriteLine("\t9 - List Patient Transactions");

                switch (ConsoleIntProcessing("Enter Menu Option: "))
                {
                    case 0:
                        return;
                    case 1:
                        //TODO: Patient Menu - Add Prescription
                        Console.Write("Prescription Name: ");
                        string rxName = Console.ReadLine();
                        int refillQuantity = ConsoleIntProcessing("Prescription Refill Count: ");
                        int pillQuantity = ConsoleIntProcessing("Prescription Quantity: ");
                        DateTime expireDate = ConsoleDateProcessing("Prescription Expire Date: ", "Expire");
                        Processing.AddPrescription(selectedPatient.PatientID, rxName, refillQuantity, pillQuantity, expireDate);
                        break;
                    case 2:
                        //TODO: Patient Menu - List Prescriptions
                        ListPrescriptionOutput(Processing.ListPrescriptionsByPatientID(selectedPatient.PatientID));
                        
                        break;
                    case 3:
                        //TODO: Patient Menu - Select Prescriptions
                        //foreach(var rx in Processing.db.Prescriptions)
                        //{
                        //    Console.WriteLine($"Rx ID: {rx.PrescriptionID} | Rx Name: {rx.PrescriptionName} | Rx Quantity: {rx.PrescriptionPillQuanity} | Rx Expire Date: {rx.PrescriptionExpireDate.ToString("d")}");
                        //}
                        ListPrescriptionOutput(Processing.ListPrescriptionsByPatientID(selectedPatient.PatientID));
                        int selection = ConsoleIntProcessing("Select Prescription ID: ");
                        if(selection != 0)
                        {
                            PatientPrescriptionMenu(Processing.db.Prescriptions.First<Prescription>(a => a.PrescriptionID == selection));
                        }
                        
                        break;
                    case 4:
                        //TODO: Patient Menu - Modify Patient
                        Console.WriteLine("\nModify Patient - Press enter to skip");
                        Patient updatedPatient = selectedPatient;
                        bool nameChange = false;
                        Console.WriteLine($"\tCurrent First Name: {selectedPatient.PatientFirstName}");
                        Console.Write("\tUpdated First Name: ");
                        string firstName = Console.ReadLine();
                        if ((firstName != "") && (firstName != selectedPatient.PatientFirstName))
                        {
                            updatedPatient.PatientFirstName = firstName;
                            nameChange = true;
                        }
                        else
                        {
                            updatedPatient.PatientFirstName = selectedPatient.PatientFirstName;
                        }

                        Console.WriteLine($"\tCurrent Last Name: {selectedPatient.PatientLastName}");
                        Console.Write("\tUpdate Last Name: ");
                        string lastName = Console.ReadLine();
                        if((lastName != "") && (lastName != selectedPatient.PatientLastName))
                        {
                            updatedPatient.PatientLastName = lastName;
                            nameChange = true;
                        }
                        else
                        {
                            updatedPatient.PatientLastName = selectedPatient.PatientLastName;
                        }

                        if(nameChange)
                        {
                            updatedPatient.PatientFullName = $"{lastName}, {firstName}";
                        }

                        Console.WriteLine($"\tCurrent Birth Date: {selectedPatient.PatientBirthDate.ToString("d")}");
                        char response = ConsoleYesNoProcessing("Modify Birthdate (Y/N): ");
                        if ( response == 'y' || response == 'Y')
                        {
                            DateTime birthDate = ConsoleDateProcessing("\tUpdate Birthdate: ", "Birth");
                            updatedPatient.PatientBirthDate = birthDate;
                        }
                        else
                        {
                            updatedPatient.PatientBirthDate = selectedPatient.PatientBirthDate;
                        }
                        
                        Processing.ModifyPatient(updatedPatient);
                        break;
                    case 5:
                        //TODO: Patient Menu - Take Prescription Dose
                        break;
                    case 8:
                        //TODO: Patient Menu - Disable Patient
                        break;
                    case 9:
                        //TODO: Patient Menu - List Patient Transactions
                        break;
                }

            }
        }

        public static void PatientPrescriptionMenu(Prescription prescription)
        {
            while(true)
            {
                Console.WriteLine("\t0 - Exit Prescription Menu");
                Console.WriteLine("\t1 - Add Refill");
                Console.WriteLine("\t2 - List Refills");
                Console.WriteLine("\t3 - Modify Refill");
                Console.WriteLine("\t");

                switch(ConsoleIntProcessing("Select Prescription Menu Option: "))
                {
                    case 0:
                        return;
                    case 1:
                        Processing.AddRefill(prescription.PrescriptionID, prescription.PrescriptionPatientID);
                        break;
                    case 2:
                        Processing.ListRefillsByPatientID(prescription.PrescriptionPatientID);
                        break;
                    case 3:
                        break;
                }

            }
        }

        

    }
}
