using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadSRT.Models
{
    public class SRT
    {
        public int SRTId {get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Translation { get; set; }

        public int Interval { get; private set; }

        public void SetInterval()
        {
            long itr = EndDate.Ticks - StartDate.Ticks;
            Interval = (EndDate.Millisecond + EndDate.Second * 1000 + EndDate.Minute * 60 * 1000) -
                (StartDate.Millisecond + StartDate.Second * 1000 + StartDate.Minute * 60 * 1000);
        }

    }
}
