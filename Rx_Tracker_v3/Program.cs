using System;

namespace Rx_Tracker_v3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Patient Prescription Tracking Application v3");
            Menu.MainMenu();
            Console.WriteLine("Terminating Execution of Program");

            Console.WriteLine("\n\nPress Enter to Exit");
            Console.ReadLine();
        }
    }
}
