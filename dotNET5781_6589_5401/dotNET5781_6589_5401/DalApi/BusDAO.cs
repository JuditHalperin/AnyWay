using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusDAO
    {
        public int License { get; set; }
        public DateTime StartOfWork { get; set; }
        public int TotalKms { get; set; }
        public int Fuel { get; set; }
        public Status Status { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
        //....
    }
}
