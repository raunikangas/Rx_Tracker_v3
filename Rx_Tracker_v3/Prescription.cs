using System;
using System.Collections.Generic;
using System.Text;

namespace Rx_Tracker_v3
{
    public class Prescription
    {
        public int PrescriptionID { get; set; }
        public int PrescriptionPatientID { get; set; }
        public string PrescriptionName { get; set; }
        public int PrescriptionPatientTrackingNumber { get; set; }
        public int PrescriptionRefillQuantity { get; set; }
        public int PrescriptionRefillRemaining { get; set; }
        public int PrescriptionPillQuanity { get; set; }
        public int PrescriptionPillDose { get; set; }
        public DateTime PrescriptionExpireDate { get; set; }
        public DateTime PrescriptionEntryDate { get; set; }
        public bool PrescriptionActive { get; set; }

        public virtual Patient Patient { get; set; }

        public Prescription()
        {

        }

        public Prescription(int patientID, string rxName, int refillQuantity, int pillQuantity)
        {
            PrescriptionEntryDate = DateTime.Now;
            PrescriptionActive = true;
            PrescriptionPillQuanity = pillQuantity;
            PrescriptionRefillQuantity = refillQuantity;
            PrescriptionPatientID = patientID;
            PrescriptionName = rxName;
        }
    }
}
