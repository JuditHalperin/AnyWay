using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLAPI
{
    class Line
    {
        private static int serial = 1;

        public int line { get; set; }

        public Regions region { get; set; }

        public int firstStation { get; set; }

        public int lastStation { get; set; }
    }
}
