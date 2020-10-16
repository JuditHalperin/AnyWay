using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_00_6589_5401
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome5401();
            Welcome6589();
            Console.ReadKey();
        }

        static partial void Welcome6589();

        private static void Welcome5401()
        {
            Console.Write("Enter your name:");
            string name = Console.ReadLine();
            Console.Write("{0}, welcome to my first console application", name);
        }
    }
}
