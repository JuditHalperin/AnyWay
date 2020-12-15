﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class Bus
    {
        public int LicensePlate { get; set; }
        public DateTime StartOfWork { get; set; }
        public DateTime LastService { get; set; }
        public int TotalKms { get; set; }
        public int KmsSinceFuel { get; set; }
        public int KmsSinceService { get; set; }
        public State Status { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}