using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMISwpf
{
    class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public override string ToString ()
        {
            return String.Format ("Group {0}: {1}", GroupId, GroupName);
        }
    }
}
