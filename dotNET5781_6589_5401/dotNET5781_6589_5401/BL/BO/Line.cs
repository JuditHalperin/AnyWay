﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Line
    {
        private static int serial = 1;
        public IEnumerable<LineStation> Path { get; set; }
        public int ThisSerial { get; set; }
        public int NumberLine { get; set; }
        public Regions Region { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}