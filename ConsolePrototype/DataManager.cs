﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

namespace ConsolePrototype
{
    class DataManager
    {
        // For connecting to database
        private const string gmis_database = "gmis";
        public const string server = "alacritas.cis.utas.edu.au";
        private const string username = "kit206g5";
        private const string password = "group5";

        private static MySqlConnection conn;

        // Master lists: Populate these by accessing database using SQL

        public List<Student> AllStudents { get; set; }
        public List<Group> AllGroups { get; set; }

        // Filtered lists: These are changed by methods which will use LINQ
        // The GUI will constantly refer to these to fill in ListBoxes etc.
        // Sometimes these will just be whole lists without filters

        public ObservableCollection<Student> FilteredStudents { get; set; }
        public ObservableCollection<Group> FilteredGroups { get; set; }

        
        // Constructor: as soon as you create a DataManager object, it will store database info into appropriate list objects
        public DataManager ()
        {
            // Log in to database
            string connectionString = String.Format ("Database={0};Data Source={1};User Id={2};Password={3}", gmis_database, server, username, password);

            // Connection to be opened when data needs to be read
            conn = new MySqlConnection (connectionString);

            // Fill the master lists
            AllStudents = LoadStudents ();
            AllGroups = LoadGroups ();
        }

        //  Steps for retrieving info in database
        //      1. declare a reader (null)
        //      2. open connection
        //      3. create command with the command itself and the connection as inputs
        //      4. create the reader by using the command.ExecuteReader() function - this is how you create a reader, dont use ' reader = new MySqlDataReader() '
        //      5. iterate through the reader and extract data into a new Student object.
        //      6. add the student to a list which is returned at the end

        /// <summary>
        /// Returns a list of all students in the database
        /// </summary>
        public List<Student> LoadStudents()
        {
            MySqlDataReader reader = null;
            List<Student> students = new List<Student> ();

            try
            {
                conn.Open ();

                MySqlCommand myCommand = new MySqlCommand ("select student_id, given_name, family_name from student", conn);

                //myCommand.Parameters.AddWithValue("number"

                reader = myCommand.ExecuteReader ();

                // while there is another row to read
                while (reader.Read ())
                {
                    // populate the students list using values found in the reader at this table row
                    students.Add (new Student
                    {
                        StudentId = reader.GetInt32 (0),
                        GivenName = reader.GetString (1),
                        FamilyName = reader.GetString (2)
                    });
                }
            }
            finally
            {
                // close the reader
                if (reader != null) {
                    reader.Close ();
                }

                // Close the connection
                if (conn != null) {
                    conn.Close ();
                }
            }

            return students;
        }

        /// <summary>
        /// Returns a list of all groups in the database
        /// </summary>
        public List<Group> LoadGroups ()
        {
            MySqlDataReader reader = null;
            List<Group> groups = new List<Group> ();

            try
            {
                conn.Open ();

                MySqlCommand myCommand = new MySqlCommand ("select * from studentGroup", conn);

                //myCommand.Parameters.AddWithValue("number"

                reader = myCommand.ExecuteReader ();

                // while there is another row to read
                while (reader.Read ())
                {
                    // populate the students list using values found in the reader at this table row
                    groups.Add (new Group
                    {
                        GroupId = reader.GetInt32 (0),
                        GroupName = reader.GetString (1),
                    });
                }
            }
            finally
            {
                // close the reader
                if (reader != null)
                {
                    reader.Close ();
                }

                // Close the connection
                if (conn != null)
                {
                    conn.Close ();
                }
            }

            return groups;
        }

        // Call this method when the GUI needs to display a subset of students
        public void FilterStudentsByGroup (int groupId)
        {
            // Use LINQ to change the FilteredStudents collection
        }

        public void FilterMeetingsByGroup (int groupId)
        {
            // Use LINQ to change FilteredMeetings
        }

        public void FilterGroupsByName (string name)
        {
            // Change FilteredGroups to only contain groups containing the 'name' string
        }

        public void PrintStudents ()
        {
            foreach (Student s in AllStudents)
            {
                Console.WriteLine (s);
            }
        }

        public void PrintGroups ()
        {
            foreach (Group g in AllGroups)
            {
                Console.WriteLine (g);
            }
        }
    }
}
