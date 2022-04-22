using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMISwpf
{
    class Meeting
    {
        public int MeetingId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public String Room { get; set; }
        public Day Day { get; set; }

        String ToString()
        {
            return MeetingId + ": From " + StartTime + " till " + EndTime + " at " + Room + " on " + Day + ".";
        }
    }
}
