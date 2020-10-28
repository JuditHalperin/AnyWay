using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_01_6589_5401
{
    class Program
    {
        static void printMenu() // print menu to user
        {
            Console.WriteLine("Hi! \n");
            Console.WriteLine("Enter 1 to add a new bus. \n");
            Console.WriteLine("Enter 2 to choose a bus to drive. \n");
            Console.WriteLine("Enter 3 to treat a bus or fuel it. \n");
            Console.WriteLine("Enter 4 to check how much km all buses drived since their last treat. \n");
            Console.WriteLine("Enter 0 to exit. \n");
        }
        enum options { exit, addBus, chooseBus, fuelingOrTreating, kmSinceTreating }
        static void Main(string[] args)
        {
            List<Bus>;

            printMenu();

            int choise;

            Console.ReadKey();
        }
    }

}
}
