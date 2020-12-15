using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DO
{
    public static class ExtensionsDO
    {
        public static BusDAO Clone(this BusDAO source)
        {
            return new BusDAO
            {
                License = source.License,
                StartOfWork = source.StartOfWork,
                TotalKms = source.TotalKms,
                Fuel = source.Fuel,
                Status = source.Status
            };
        }

        public static BusInTravelDAO Clone(this BusInTravelDAO source)
        {
            return new BusInTravelDAO
            {
                License = source.License,
                Line = source.Line,
                Start = source.Start
            };
        }

    }
}