using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

namespace GMISwpf
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
        public List<Meeting> AllMeetings { get; set; }
        public List<Class> AllClasses { get; set; }

        //meetings and class

        // Filtered lists: These are changed by methods which will use LINQ
        // The GUI will constantly refer to these to fill in ListBoxes etc.
        // Sometimes these will just be whole lists without filters

        public ObservableCollection<Student> FilteredStudents { get; set; }
        public ObservableCollection<Group> FilteredGroups { get; set; }
        public ObservableCollection<Meeting> FilteredMeetings { get; set; }
        public ObservableCollection<Class> FilteredClasses { get; set; }

        public Student CurrentStudent { get; set; }
        public Meeting CurrentMeeting { get; set; }

        // Constructor: as soon as you create a DataManager object, it will store database info into appropriate list objects
        public DataManager ()
        {
            Console.WriteLine ("DataManager created");
            
            // Log in to database
            string connectionString = String.Format ("Database={0};Data Source={1};User Id={2};Password={3};Convert Zero Datetime=True", gmis_database, server, username, password);

            // Connection to be opened when data needs to be read
            conn = new MySqlConnection (connectionString);

            // Fill the master lists
            AllStudents = LoadStudents ();
            AllGroups = LoadGroups ();
            AllMeetings = LoadMeetings ();
            AllClasses = LoadClasses ();

            //Filtered lists start as copies of master lists
            FilteredStudents = new ObservableCollection<Student> (AllStudents);
            FilteredGroups = new ObservableCollection<Group> (AllGroups);
            FilteredMeetings = new ObservableCollection<Meeting> (AllMeetings);
            FilteredClasses = new ObservableCollection<Class> (AllClasses);
        }

        // Provided by UTAS, convert string to corresponding Enum value
        public static T ParseEnum<T> (string value)
        {
            return (T)Enum.Parse (typeof (T), value);
        }

        // Get methods, for data bindings
        public ObservableCollection<Student> GetFilteredStudents()
        {
            return FilteredStudents;
        }

        public ObservableCollection<Group> GetFilteredGroups ()
        {
            return FilteredGroups;
        }

        public ObservableCollection<Meeting> GetFilteredMeetings ()
        {
            return FilteredMeetings;
        }

        public ObservableCollection<Class> GetFilteredClasses ()
        {
            return FilteredClasses;
        }

        // Load methods fill the master lists with data from the gmis database

        public List<Class> LoadClasses()
        {
            MySqlDataReader reader = null;
            List<Class> classes = new List<Class>();

            try
            {
                conn.Open();

                MySqlCommand myCommand = new MySqlCommand("select * from class", conn);

                //myCommand.Parameters.AddWithValue("number"

                reader = myCommand.ExecuteReader();

                // while there is another row to read
                while (reader.Read())
                {
                    // populate the students list using values found in the reader at this table row
                    classes.Add(new Class
                    {
                        ClassID = reader.GetInt32 (0),
                        GroupID = reader.GetInt32 (1),
                        Day = ParseEnum<Day> (reader.GetString (2)),
                        StartTime = reader.GetString(3),
                        EndTime = reader.GetString(4),
                        Room = reader.GetString(5)

                    });
                }
            }
            finally
            {
                // close the reader
                if (reader != null)
                {
                    reader.Close();
                }

                // Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return classes;
        }

        public List<Meeting> LoadMeetings()
        {
            MySqlDataReader reader = null;
            List<Meeting> meetings = new List<Meeting>();

            try
            {
                conn.Open();

                MySqlCommand myCommand = new MySqlCommand("select * from meeting", conn);

                //myCommand.Parameters.AddWithValue("number"

                reader = myCommand.ExecuteReader();

                // while there is another row to read
                while (reader.Read())
                {
                    meetings.Add(new Meeting
                    {
                        MeetingId = reader.GetInt32(0),
                        GroupID = reader.GetInt32 (1),
                        Day = ParseEnum<Day> (reader.GetString (2)),
                        StartTime = reader.GetString(3),
                        EndTime = reader.GetString(4),
                        Room = reader.GetString(5)
                        
                    });
                }
            }
            finally
            {
                // close the reader
                if (reader != null)
                {
                    reader.Close();
                }

                // Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return meetings;
        }

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

                MySqlCommand myCommand = new MySqlCommand ("select * from student", conn);

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
                        FamilyName = reader.GetString (2),
                        GroupId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3), // If student is in group NULL then assign group 0 (no group)
                        Phone = reader.GetString(6),
                        Email = reader.GetString (7)
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

        // method for updating the lists, to be used after the database has been modifed
        public void ReloadAll()
        {
            AllStudents = LoadStudents ();
            AllGroups = LoadGroups ();
            AllMeetings = LoadMeetings ();
            AllClasses = LoadClasses ();
        }

        public void insertMeeting(int groupid, string day, string start, string end, string room)
        {
            // insert into database
            try
            {
                conn.Open ();

                string command = "INSERT INTO meeting (group_id, day, start, end, room) VALUES ('" + groupid + "', '" + day + "', '" + start + "', '"+ end + "', '" + room + "')";
                
                Console.WriteLine(command);
                
                MySqlCommand myCommand = new MySqlCommand(command, conn);

                myCommand.ExecuteNonQuery();
                
                Console.WriteLine ("DATABASE MODIFIED - ADDED MEETING");
                
            }
            finally
            {
                conn.Close();
            }

            ReloadAll ();
        }

        public void UpdateMeeting(int meetingID, string day, string start, string end, string room)
        {
            // insert into database
            try
            {
                conn.Open();

                string command = String.Format("UPDATE meeting SET day='{0}', start='{1}', end='{2}', room='{3}' WHERE meeting_id={4}",
                                                   day, start, end, room, meetingID);
                Console.WriteLine(command);
                
                MySqlCommand myCommand = new MySqlCommand(command, conn);

                myCommand.ExecuteNonQuery();
                
                Console.WriteLine ("DATABASE MODIFIED - MEETING UPDATED");
                
            }
            finally
            {
                conn.Close();
            }

            ReloadAll ();

        }

        public void UpdateStudent (int id, string newTitle, string newCampus, string newPhone, string newEmail, string newCategory)
        {
            // insert into database
            try
            {
                conn.Open ();

                string command = String.Format ("UPDATE student SET title='{1}', campus='{2}', phone='{3}', email='{4}', category='{5}' WHERE student_id={0}",
                                                   id, newTitle, newCampus, newPhone, newEmail, newCategory);

                Console.WriteLine (command);
                
                MySqlCommand myCommand = new MySqlCommand (command, conn);

                myCommand.ExecuteNonQuery ();
                
                Console.WriteLine ("DATABASE MODIFIED - PROFILE CREATED");
                
            }
            finally
            {
                conn.Close ();
            }

            ReloadAll ();
        }

        public void UpdateStudentGroup (int id, int newGroup)
        {
            // insert into database
            try
            {
                conn.Open ();

                string command = String.Format ("UPDATE student SET group_id={0} WHERE student_id={1}", newGroup, id);

                Console.WriteLine (command);
                
                MySqlCommand myCommand = new MySqlCommand (command, conn);

                myCommand.ExecuteNonQuery ();
                
                Console.WriteLine ("DATABASE MODIFIED - STUDENT GROUP CHANGE");
                CurrentStudent.GroupId = newGroup;
                
            }
            finally
            {
                conn.Close ();
            }

            ReloadAll ();
        }

        public void insertGroup(string groupname)
        {
            // insert into database
            try
            {
                conn.Open();

                string command = String.Format("INSERT INTO studentGroup (group_name) VALUES ('" + groupname + "')");
                MySqlCommand myCommand = new MySqlCommand(command, conn);

                Console.WriteLine (command);
                myCommand.ExecuteNonQuery();

                Console.WriteLine ("DATABASE MODIFIED - GROUP ADDED");
                
            }
            finally
            {
                conn.Close();
            }

            ReloadAll ();
        }

        public void UpdateGroup(int groupID, string groupname)
        {
            // insert into database
            try
            {
                conn.Open();

                string command = String.Format("UPDATE studentGroup SET group_name='{1}' WHERE group_id={0}",
                                                   groupID, groupname);
                Console.WriteLine (command);
                MySqlCommand myCommand = new MySqlCommand(command, conn);

                myCommand.ExecuteNonQuery();
                Console.WriteLine ("DATABASE MODIFIED - GROUP UPDATED");
                
            }
            finally
            {
                conn.Close ();
            }

            ReloadAll ();
        }

        public void DeleteMeeting (int meetingID) {
            try
            {
                conn.Open ();

                string command = "DELETE FROM meeting WHERE meeting_id=" + meetingID;
                Console.WriteLine (command);
                MySqlCommand myCommand = new MySqlCommand (command, conn);

                myCommand.ExecuteNonQuery ();
                Console.WriteLine ("DATABASE MODIFIED - MEETING DELETED");

            }
            finally
            {
                conn.Close ();
            }

            ReloadAll ();
        }

        // Call this method when the GUI needs to display a subset of students
        public void FilterStudentsByGroup (int groupId)
        {
            var filtered = from s in AllStudents
                           where s.GroupId == groupId
                           select s;

            FilteredStudents.Clear ();

            foreach(Student s in filtered)
            {
                FilteredStudents.Add (s);
            }
        }

        public void UnfilterStudents ()
        {
            FilteredStudents.Clear ();

            foreach (Student s in AllStudents)
            {
                FilteredStudents.Add (s);
            }
        }

        public void FilterMeetingsByGroup (int groupId)
        {
            var filtered = from s in AllMeetings
                           where s.GroupID == groupId
                           select s;

            FilteredMeetings.Clear();

            foreach (Meeting s in filtered)
            {
                FilteredMeetings.Add(s);
            }
        }

        /*public void FilterGroupsByName (string name)
        {
            var filtered = from s in AllGroups
                           where s.GroupName == name
                           select s;

            FilteredGroups.Clear ();

            foreach (Group s in filtered)
            {
                FilteredGroups.Add(s);
            }
        }*/

        public void UnfilterGroups ()
        {
            FilteredGroups.Clear ();

            foreach (Group s in AllGroups)
            {
                FilteredGroups.Add (s);
            }
        }

        public void FilterClassesByGroup (int groupId)
        {
            var filtered = from s in AllClasses
                           where s.GroupID == groupId
                           select s;

            FilteredClasses.Clear ();

            foreach (Class s in filtered)
            {
                FilteredClasses.Add (s);
            }

            if (FilteredClasses.Count == 0)
            {
                FilteredClasses.Add (new Class
                {
                    Room = "No class added"
                });
            }
        }

        public string GetGroupNameFromId (int id)
        {
            string name = "Invalid ID";
            
            var group = from g in AllGroups
                        where g.GroupId == id
                        select g;

            if (group != null) name = group.First ().GroupName;

            return name;
        }

        public int GetGroupIdFromName (string name)
        {
            int id = 0;

            var group = from g in AllGroups
                        where g.GroupName == name
                        select g;

            if (group != null) id = group.First ().GroupId;

            return id;
        }
    }
}
