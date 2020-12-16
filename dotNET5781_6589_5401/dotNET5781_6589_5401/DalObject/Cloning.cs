using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DL
{
    static class Cloning // one of three
    {
        //internal static IClonable Clone(this IClonable original)//דרך שניה - בונוס (יש להשתמש בממשק)
        //{
        //    IClonable target = (IClonable)Activator.CreateInstance(original.GetType());
        //    //...
        //    return target;
        //}

        //internal static T Clone<T>(this T original)//דרך שלישית - בונוס
        //{
        //    T target = (T)Activator.CreateInstance(original.GetType());
        //    //...
        //    return target;
        //}

        internal static Bus Clone(this Bus original)
        {
            Bus target = new Bus();
            target.LicensePlate = original.LicensePlate;
            target.StartOfWork = original.StartOfWork;
            target.Status = original.Status;
            target.TotalKms = original.TotalKms;
            target.KmsSinceFuel = original.KmsSinceFuel;
            target.KmsSinceService = original.KmsSinceService;
            target.LastService = original.LastService;
            return target;
        }

        internal static Line Clone(this Line original)
        {
            Line target = new Line();
            target.ThisSerial = original.ThisSerial;
            target.NumberLine = original.NumberLine;
            target.FirstStation = original.FirstStation;
            target.LastStation = original.LastStation;
            target.Region = original.Region;
            return target;
        }

        internal static Station Clone(this Station original)
        {
            Station target = new Station();
            target.ID = original.ID;
            target.Name = original.Name;
            target.Latitude = original.Latitude;
            target.Longitude = original.Longitude;
            return target;
        }
    }
}