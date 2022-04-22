using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMISwpf
{
    class Class
    {
        public int ClassID { get; set; }
        public int GroupID { get; set; }
        public Day Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Room { get; set; }
        

        public override string ToString()
        {
            return ClassID + ": From " + StartTime + " till " + EndTime + " at " + Room + " on " + Day + ".";
        }
    }
}
