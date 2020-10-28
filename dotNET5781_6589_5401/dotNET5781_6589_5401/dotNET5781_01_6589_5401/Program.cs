using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_01_6589_5401
{
    class Program
    {
        enum b{ addBus,chooseBus, fuelingOrTreating, kmSinceTreating, exit}
        static void Main(string[] args)
        {
            // תאריך בודק תקינות?
            // לבנות בנאי ריק לאוטובוס?
            // אם נוצר אוטוטבוס שגוי?

            // חלק פונקציות כפרטיות
            // תאריך לא גדול מ NOW
            // לאתחל DateOfLastTreat

            DateTime d = new DateTime(2015, 7, 8);
            string i = "123456a";

            //Bus bus = new Bus(d, i);

            //Console.WriteLine(bus.Id);
            //Console.WriteLine(bus.DateOfBegining);
            List<Bus>;
            Console.ReadKey();
        }
    }
}
