using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BLAPI;
using BO;

namespace PlConsole
{
    class Program
    {
        static IBL bl;

        static void Main(string[] args)
        {
            bl = BlFactory.GetBl();

            Console.Write("Please enter how many days back: ");
            foreach(var i in bl.GetLineStations())
            {
                Console.WriteLine(i);
            }
            Console.Read();
        }
    }
}
