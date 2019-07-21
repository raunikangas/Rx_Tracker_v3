using System;
using System.Collections.Generic;
using System.Text;

namespace Rx_Tracker_v3
{
    public class Menu
    {
        public int ConsoleIntProcessing(string consoleOutput)
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

                }
            }
        }

        


        public void MainMenu()
        {


        }




    }
}
