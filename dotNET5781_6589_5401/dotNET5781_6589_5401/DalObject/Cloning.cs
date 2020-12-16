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
        
        //cloning of the object...:
        /// <summary>
        /// Cloning Bus
        /// </summary>
        /// <param name="original">Copy from this</param>
        /// <returns>Return the copy bus</returns>
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
        /// <summary>
        /// Cloning Line
        /// </summary>
        /// <param name="original">Copy from this</param>
        /// <returns>Return the copy line</returns>
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
        /// <summary>
        /// Cloning Station
        /// </summary>
        /// <param name="original">Copy from this</param>
        /// <returns>Return the copy station</returns>
        internal static Station Clone(this Station original)
        {
            Station target = new Station();
            target.ID = original.ID;
            target.Name = original.Name;
            target.Latitude = original.Latitude;
            target.Longitude = original.Longitude;
            return target;
        }
        /// <summary>
        /// Cloning LineStation
        /// </summary>
        /// <param name="original">Copy from this</param>
        /// <returns>Return the copy LineStation</returns>
        internal static LineStation Clone(this LineStation original)
        {
            LineStation target = new LineStation();
            target.ID = original.ID;
            target.NumberLine = original.NumberLine;
            target.PathIndex = original.PathIndex;
            return target;
        }
        /// <summary>
        /// Cloning DrivingBus
        /// </summary>
        /// <param name="original">Copy from this</param>
        /// <returns>Return the copy DrivingBus</returns>
        internal static DrivingBus Clone(this DrivingBus original)
        {
            DrivingBus target = new DrivingBus();
            target.ThisSerial = original.ThisSerial;
            target.Line = original.Line;
            target.LicensePlate = original.LicensePlate;
            target.ActualStart = original.ActualStart;
            target.Start = original.Start;
            target.PreviousStationID = original.PreviousStationID;
            target.PreviousStationTime = original.PreviousStationTime;
            target.NextStationTime = original.NextStationTime;
            return target;
        }
        /// <summary>
        /// Cloning DrivingLine
        /// </summary>
        /// <param name="original">Copy from this</param>
        /// <returns>Return the copy DrivingLine</returns>
        internal static DrivingLine Clone(this DrivingLine original)
        {
            DrivingLine target = new DrivingLine();
            target.NumberLine = original.NumberLine;
            target.Start = original.Start;
            target.End = original.End;
            target.Frequency = original.Frequency;
            return target;
        }
        /// <summary>
        /// Cloning TwoFollowingStations
        /// </summary>
        /// <param name="original">Copy from this</param>
        /// <returns>Return the copy TwoFollowingStations</returns>
        internal static TwoFollowingStations Clone(this TwoFollowingStations original)
        {
            TwoFollowingStations target = new TwoFollowingStations();
            target.FirstStationID = original.FirstStationID;
            target.SecondStationID = original.SecondStationID;
            target.LengthBetweenStations = original.LengthBetweenStations;
            target.TimeBetweenStations = original.TimeBetweenStations;
            return target;
        }
        /// <summary>
        /// Cloning User
        /// </summary>
        /// <param name="original">Copy from this</param>
        /// <returns>Return the copy User</returns>
        internal static User Clone(this User original)
        {
            User target = new User();
            target.Username = original.Username;
            target.Password = original.Password;
            target.IsManager = original.IsManager;
            return target;
        }
    }
}