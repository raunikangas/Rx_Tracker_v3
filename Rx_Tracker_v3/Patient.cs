using System;
using System.Collections.Generic;
using System.Text;

namespace Rx_Tracker_v3
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientFullName { get; set; }
        public DateTime PatientBirthDate { get; set; }
        public DateTime PatientEnteredDate { get; set; }
        public string PatientEmail { get; set; }
        public string PatientPhoneNumber { get; set; }
        public bool PatientActive { get; set; }

        public Patient()
        {

        }

        public Patient(string fName, string lName, string email, DateTime birthDate)
        {
            PatientFirstName = fName;
            PatientLastName = lName;
            PatientFullName = $"{lName}, {fName}";
            PatientBirthDate = birthDate;
            PatientEmail = email;
            PatientActive = true;
            PatientEnteredDate = DateTime.Now;
        }


    }
}
