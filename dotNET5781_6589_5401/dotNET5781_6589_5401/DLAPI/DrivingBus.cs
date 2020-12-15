using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class DrivingBus
    {
        private static int serial = 1;
        public int LicensePlate { get; set; }
        public int Line { get; set; }
        public DateTime Start { get; set; }
    }
}
