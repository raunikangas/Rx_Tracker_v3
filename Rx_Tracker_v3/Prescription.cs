﻿using System;
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
        public int PrescriptionPillQuantity { get; set; }
        public int PrescriptionPillQuantityRemaining { get; set; }
        public int PrescriptionPillDose { get; set; }
        public DateTime PrescriptionExpireDate { get; set; }
        public DateTime PrescriptionEntryDate { get; set; }
        public DateTime PrescriptionNextRefillEnableDate { get; set; }
        public bool PrescriptionActive { get; set; }

        public virtual Patient Patient { get; set; }

        public Prescription()
        {

        }

        public Prescription(int patientID, int patientTrackingID, string rxName, int refillQuantity, int pillQuantity, int individualDose, DateTime expireDate)
        {
            PrescriptionEntryDate = DateTime.Now;
            PrescriptionActive = true;
            PrescriptionPillQuantity = pillQuantity;
            PrescriptionPillQuantityRemaining = pillQuantity;
            PrescriptionRefillQuantity = refillQuantity;
            PrescriptionRefillRemaining = refillQuantity;
            PrescriptionPatientID = patientID;
            PrescriptionName = rxName;
            PrescriptionExpireDate = expireDate;
            PrescriptionPatientTrackingNumber = patientTrackingID;
            PrescriptionPillDose = individualDose;

            if(refillQuantity == 0)
            {
                PrescriptionNextRefillEnableDate = expireDate;
            }
            else
            {
                PrescriptionNextRefillEnableDate = DateTime.Now.AddDays(Convert.ToInt32((pillQuantity/individualDose) + ((pillQuantity/individualDose)*0.5)));
            }
            
        }
    }
}
