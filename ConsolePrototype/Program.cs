using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConsolePrototype
{
    // Enums are at the bottom

    class Program
    {
        static void Main (string[] args)
        {
            DataManager theManager = new DataManager ();

            theManager.PrintStudents ();
            Console.WriteLine ("---------------");
            theManager.PrintGroups ();
        }
    }

    // Enums sit at namespace level, not inside any class, so they'll be recognised anywhere in the namespace
    enum Campus
    {
        Hobart,
        Launceston
    }

    enum Category
    {
        Bachelors,
        Masters
    }

    enum Day
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

}
