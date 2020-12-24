using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            IEnumerable<LineStation> lineStations = bl.GetLineStations();
            Console.WriteLine(lineStations.ToStringProperty());
            Console.WriteLine(lineStations.ToString());
            List<Line> lines = (List < Line > )bl.GetLines(item=>true);
            Console.WriteLine(lines.ToStringProperty());
            Console.WriteLine(lines.ToString());
            //for (int i = 0; i < lines.Count; i++)
            //{
            //    Console.WriteLine(lines[i].NumberLine);
            //    Console.WriteLine(lines[i].Region);
            //    Console.WriteLine(lines[i].ThisSerial);
            //    Console.WriteLine(lines[i].Path);

            //}
            Console.Read();
        }
    }
}
