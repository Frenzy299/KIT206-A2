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
        public int GroupID { get; set; }

        public override string ToString()
        {
            return String.Format("{0} in room {1} from {2} till {3}", Day, Room, StartTime, EndTime);
        }
    }
}
