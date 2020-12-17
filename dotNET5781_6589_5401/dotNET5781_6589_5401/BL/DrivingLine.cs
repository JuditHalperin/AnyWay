using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DrivingLine
    {
        #region
        private int numberLine; public int NumberLine
        {
            get { return numberLine; }
            private set { numberLine = value; }
        }
        private DateTime start; public DateTime Start
        {
            get { return start; }
            private set { start = value; }
        }
        private int frequency; public int Frequency
        {
            get { return frequency; }
            private set { frequency = value; }
        }
        private DateTime end; public DateTime End
        {
            get { return end; }
            private set { end = value; }
        }

        #endregion

        public DrivingLine(int numberLine, DateTime start, int frequency, DateTime end)
        {
            NumberLine = numberLine;
            Start = start;
            Frequency = frequency;
            End = end;
        }
    }
}