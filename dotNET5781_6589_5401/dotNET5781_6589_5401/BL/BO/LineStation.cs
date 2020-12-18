﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineStation
    {
        public int NumberLine { get; set; }
        public int ID { get; set; }
        public int PathIndex { get; set; }
        public double LengthFromPreviousStations { get; set; }
        public int TimeFromPreviousStations { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}