using System;
using System.Collections.Generic;
using System.Text;

namespace Rx_Tracker_v3
{
    public class Refill
    {
        public int RefillID { get; set; }
        public int RefillRxID { get; set; }
        public int RefillPatientID { get; set; }
        public DateTime RefillEntryDate { get; set; }
        public bool RefillFilled { get; set; }
        public DateTime RefillFillDate { get; set; }


        public virtual Patient Patient { get; set; }
        public virtual Prescription Prescription { get; set; }

        public Refill()
        {

        }

        public Refill(int rxID, int patientID)
        {
            RefillRxID = rxID;
            RefillPatientID = patientID;
            RefillEntryDate = DateTime.Now;
            RefillFilled = false;
        }
    }
}
