using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePrototype
{
    class Student
    {
        public int StudentId { get; set; }      // 11 digits

        public string GivenName { get; set; }   // 20 chars
        public string FamilyName { get; set; }  // 20 chars

        public int GroupId { get; set; }        // 11 digits
        public string Title { get; set; }       // 10 chars

        public Campus Campus { get; set; }

        public string Phone { get; set; }       // 15 chars
        public string Email { get; set; }       // 50 chars
        
        //public Image?? Photo { get; set; }

        public Category Category { get; set; }

        public override string ToString ()
        {
            return String.Format ("ID: {0}, name is {1} {2}.", StudentId, GivenName, FamilyName);
        }
    }

    
}
