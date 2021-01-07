﻿using System;
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

            if(bl.GetLines().Count() == 0)
                Console.WriteLine("jjj");
            Console.WriteLine("oof");

            foreach (var i in bl.GetTrips())
            {
                Console.WriteLine(i);
            }
            Console.Read();
        }
    }
}
