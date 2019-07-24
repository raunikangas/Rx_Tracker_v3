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
            foreach (var patient in patientsList)
            {
                Console.WriteLine($"Patient ID: {patient.PatientID} | Patient Name: {patient.PatientFullName} | Patient Birthdate: {patient.PatientBirthDate.ToString("d")}");
            }
        }

        public static void ListPrescriptionOutput(IEnumerable<Prescription> prescriptionList)
        {
            foreach(var rx in prescriptionList)
            {
                Console.WriteLine($"Rx ID: {rx.PrescriptionID} | Rx Name: {rx.PrescriptionName} | Rx Quantity: {rx.PrescriptionPillQuanity} | Rx Expire Date: {rx.PrescriptionExpireDate.ToString("d")}");
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
                            DateTime birthDate = ConsoleDateProcessing("\tPatient Birth Date: ", "Birth");

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
                            ListPatientOutput(patientsList);
                        }
                        else
                        {
                            Console.WriteLine("[!] No Patients to List");
                        }
                        break;
                    case 3:
                        //TODO: Menu - Select Patients
                        if(Processing.db.Patients.Count() > 0)
                        {
                            ListPatientOutput(Processing.ListPatient());
                        }

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
                        ListPrescriptionOutput(Processing.ListPrescriptions(selectedPatient[0].PatientID));
                        
                        break;
                    case 3:
                        //TODO: Patient Menu - Select Prescriptions
                        //foreach(var rx in Processing.db.Prescriptions)
                        //{
                        //    Console.WriteLine($"Rx ID: {rx.PrescriptionID} | Rx Name: {rx.PrescriptionName} | Rx Quantity: {rx.PrescriptionPillQuanity} | Rx Expire Date: {rx.PrescriptionExpireDate.ToString("d")}");
                        //}
                        ListPrescriptionOutput(Processing.ListPrescriptions(selectedPatient[0].PatientID));
                        int selection = ConsoleIntProcessing("Select Prescription ID: ");
                        if(selection != 0)
                        {
                            PatientPrescriptionMenu(Processing.db.Prescriptions.Where(a => a.PrescriptionID == selection));
                        }
                        
                        break;
                    case 4:
                        //TODO: Patient Menu - Modify Patient
                        Console.WriteLine("\nModify Patient - Press enter to skip");
                        Patient updatedPatient = selectedPatient[0];
                        bool nameChange = false;
                        Console.WriteLine($"\tCurrent First Name: {selectedPatient[0].PatientFirstName}");
                        Console.Write("\tUpdated First Name: ");
                        string firstName = Console.ReadLine();
                        if ((firstName != "") && (firstName != selectedPatient[0].PatientFirstName))
                        {
                            updatedPatient.PatientFirstName = firstName;
                            nameChange = true;
                        }
                        else
                        {
                            updatedPatient.PatientFirstName = selectedPatient[0].PatientFirstName;
                        }

                        Console.WriteLine($"\tCurrent Last Name: {selectedPatient[0].PatientLastName}");
                        Console.Write("\tUpdate Last Name: ");
                        string lastName = Console.ReadLine();
                        if((lastName != "") && (lastName != selectedPatient[0].PatientLastName))
                        {
                            updatedPatient.PatientLastName = lastName;
                            nameChange = true;
                        }
                        else
                        {
                            updatedPatient.PatientLastName = selectedPatient[0].PatientLastName;
                        }

                        if(nameChange)
                        {
                            updatedPatient.PatientFullName = $"{lastName}, {firstName}";
                        }

                        Console.WriteLine($"\tCurrent Birth Date: {selectedPatient[0].PatientBirthDate.ToString("d")}");
                        char response = ConsoleYesNoProcessing("Modify Birthdate (Y/N): ");
                        if ( response == 'y' || response == 'Y')
                        {
                            DateTime birthDate = ConsoleDateProcessing("\tUpdate Birthdate: ", "Birth");
                            updatedPatient.PatientBirthDate = birthDate;
                        }
                        else
                        {
                            updatedPatient.PatientBirthDate = selectedPatient[0].PatientBirthDate;
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
                    case 0:
                        break;
                    case 1:
                        break;
                }

            }
        }

    }
}
