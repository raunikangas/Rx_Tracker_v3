using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Rx_Tracker_v3
{
    public static class Menu
    {
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
        
        public static void ListPatient(IEnumerable<Patient> patientsList)
        {
            foreach (var patient in patientsList)
            {
                Console.WriteLine($"Patient ID: {patient.PatientID} | Patient Name: {patient.PatientFullName} | Patient Birthdate: {patient.PatientBirthDate}");
            }
        }

        public static void MainMenu()
        {
            while(true)
            {
                Console.WriteLine("\t0 - Exit Application");
                Console.WriteLine("\t1 - Add Patient");
                Console.WriteLine("\t2 - List Patient");
                Console.WriteLine("\t3 - Select Patients");
                Console.WriteLine("\t4 - ");
                Console.WriteLine("\t10 - List All Transactions");

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
                            Console.WriteLine("\tPatient Birth Date: ");
                            int month = ConsoleIntProcessing("\t\tBirth Month (Ex. 01): ");
                            int day = ConsoleIntProcessing("\t\tBirth Day (Ex. 01): ");
                            int year = ConsoleIntProcessing("\t\tBirth Year (Ex. 1980): ");
                        
                            DateTime birthDate = Convert.ToDateTime($"{month}/{day}/{year}");

                            Processing.AddPatient(firstName, lastName, birthDate);
                            Console.WriteLine("Patient Added to Database");
                        }
                        catch(FormatException)
                        {

                        }
                        break;
                    case 2:
                        //TODO: Menu - List Patients
                        var patientsList = Processing.ListPatient();
                        if(patientsList.Count() > 0)
                        {
                            ListPatient(patientsList);
                        }
                        else
                        {
                            Console.WriteLine("[!] No Patients to List");
                        }
                        break;
                    case 3:
                        //TODO: Menu - Select Patients


                        List<Patient> selectedPatient = Processing.SelectPatient(ConsoleIntProcessing("Select Patient ID: "));

                        PatientMenu(selectedPatient);
                        break;
                    case 4:
                        //TODO: Menu - 
                        break;
                    case 10:
                        //TODO: Menu - Create List All Transaction
                        break;
                }
            }
            
        }

        public static void PatientMenu(List<Patient> selectedPatient)
        {
            while (true)
            {
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

                        Processing.AddPrescription(selectedPatient[0].PatientID, rxName, refillQuantity, pillQuantity);
                        break;
                    case 2:
                        //TODO: Patient Menu - List Prescriptions
                        var returnedPrescriptions = Processing.ListPrescriptions(selectedPatient[0].PatientID);
                        foreach(var rx in returnedPrescriptions)
                        {
                            Console.WriteLine($"Rx ID: {rx.PrescriptionID} | Rx Name: {rx.PrescriptionName} | Rx Quantity: {rx.PrescriptionPillQuanity} | Rx Expire Date: {rx.PrescriptionExpireDate.ToString("d")}");
                        }
                        break;
                    case 3:
                        //TODO: Patient Menu - Select Prescriptions
                        foreach(var rx in Processing.db.Prescriptions)
                        {
                            Console.WriteLine($"Rx ID: {rx.PrescriptionID} | Rx Name: {rx.PrescriptionName} | Rx Quantity: {rx.PrescriptionPillQuanity} | Rx Expire Date: {rx.PrescriptionExpireDate.ToString("d")}");
                        }
                        int selection = ConsoleIntProcessing("Select Prescription ID: ");
                        if(selection != 0)
                        {
                            PatientPrescriptionMenu(Processing.db.Prescriptions.Where(a => a.PrescriptionID == selection));
                        }
                        
                        break;
                    case 4:
                        //TODO: Patient Menu - Modify Patient
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

        public static void PatientPrescriptionMenu(IEnumerable<Prescription> prescription)
        {
            while(true)
            {
                Console.WriteLine("\t0 - Exit Prescription Menu");
                Console.WriteLine("\t1 - Add Refill");
                Console.WriteLine("\t2 - List Refills");
                Console.WriteLine("\t3 - Modify Prescription");
                Console.WriteLine("\t");

                switch(ConsoleIntProcessing("Select Prescription Menu Option: "))
                {

                }

            }
        }

    }
}
