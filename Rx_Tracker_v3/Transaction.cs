using System;
using System.Collections.Generic;
using System.Text;

namespace Rx_Tracker_v3
{
    public enum ClassesEnum { Patient, Prescription, Refill }
    public enum ClassAction { Add, Modify, Delete, Disable }
    public enum ID_Type { Patient, Prescription, Refill}

    public class Transaction
    {
        public int TransactionID { get; set; }
        public string TransactionAction { get; set; }
        public ClassesEnum TransactionClass { get; set; }
        public ClassAction TransactionClassAction { get; set; }
        public DateTime TransactionEntryDateTime { get; set; }
        public int TransactionPatientID { get; set; }
        public ID_Type TransactionIDType { get; set; }

        public Transaction()
        {

        }

        /// <summary>
        /// Transaction Logging - Patient Transaction Logging
        /// </summary>
        /// <param name="classAction">Action Taken (Add, Modify, Disable, Delete)</param>
        /// <param name="classes">Action Class (Patient, Prescription, Refill)</param>
        /// <param name="idType">Type of ID that is being tracked</param>
        /// <param name="idNumber">ID Number of ID Type</param>
        /// <param name="loggedAction">String action for what was done</param>
        public Transaction(ClassAction action, ClassesEnum classes, ID_Type idType, int idNumber, string loggedAction)
        {
            TransactionClass = classes;
            TransactionClassAction = action;
            TransactionAction = loggedAction;

            TransactionPatientID = idNumber;
            TransactionEntryDateTime = DateTime.Now;
        }

        public Transaction(ClassAction action, ClassesEnum classes, string loggedAction)
        {
            TransactionClass = classes;
            TransactionClassAction = action;
            TransactionAction = loggedAction;
            TransactionEntryDateTime = DateTime.Now;
        }
    }
}
