using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GMISwpf
{
    /// <summary>
    /// Interaction logic for EditMeeting.xaml
    /// </summary>
    public partial class EditMeeting : Page
    {
        private DataManager theManager;
        public EditMeeting()
        {
            InitializeComponent();
            //MeetingDateTime.Text = theManager.CurrentMeeting.MeetingId.ToString();
            theManager = (DataManager)Application.Current.FindResource("datamanager");
            MeetingPanel.DataContext = theManager.CurrentMeeting;
            Day.Items.Add("Monday");
            Day.Items.Add("Tuesday");
            Day.Items.Add("Wednesday");
            Day.Items.Add("Thursday");
            Day.Items.Add("Friday");
            Day.Items.Add("Saturday");
            Day.Items.Add("Sunday");
            for (int i = 1; i <= 12; i++) { StartHours.Items.Add(i); EndHours.Items.Add(i); }
            for (int i = 0; i <= 59; i++) { StartMinutes.Items.Add(i); EndMinutes.Items.Add(i); }

            //display current meeting id in MeetingDetails
            //display current meeting date/time in MeetingDateTime
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            //in database:
            //change day of current meeting to the date selected in Day Month
            //change start time of current meeting to the time selected in StartHour, StartMinutes and StartAMPM
            //change start time of current meeting to the time selected in EndHour, EndMinutes and EndAMPM
            //change room of current meeting to room displayed in Room
 
            string newDay = Day.Text;
            string newStart = StartHours.Text + ":" + StartMinutes.Text + ":00";
            string newEnd = EndHours.Text + ":" + EndMinutes.Text + ":00";
            string newRoom = Room.Text;
            int meetingToUpdate = theManager.CurrentMeeting.MeetingId;

            

            theManager.UpdateMeeting(meetingToUpdate, newDay, newStart, newEnd, newRoom);
            //theManager.CurrentMeeting.MeetingId = newGroupId;


            MainWindow objMainWindow = (MainWindow)Window.GetWindow(this);

            objMainWindow.Content = new Home();
        }

        private void CancelMeeting_Click(object sender, RoutedEventArgs e)
        {
            //Remove current meeting from database

            if (MessageBox.Show ("Are you sure you want to delete this meeting?", "Cancel meeting", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                theManager.DeleteMeeting (theManager.CurrentMeeting.MeetingId);
                
                MainWindow objMainWindow = (MainWindow)Window.GetWindow (this);
                objMainWindow.Content = new Home ();
            }

            //MainWindow objMainWindow = (MainWindow)Window.GetWindow(this);
            //objMainWindow.Content = new Home();
        }

        private void Room_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BackButton_Click (object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = (MainWindow)Window.GetWindow (this);
            objMainWindow.Content = new Home ();
        }
    }
}
