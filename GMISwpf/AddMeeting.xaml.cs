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
    /// Interaction logic for AddMeeting.xaml
    /// </summary>
    public partial class AddMeeting : Page
    {
        private DataManager theManager;
        public AddMeeting()
        {
            InitializeComponent();
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
 
        }

        private void StartHours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EndMinutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Day_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            int NewGroupid = theManager.CurrentStudent.GroupId;
            string NewStart = StartHours.Text + ":" + StartMinutes.Text+ ":00";
            string NewEnd = EndHours.Text + ":" + EndMinutes.Text + ":00";
            string newDay = Day.Text;
            string newRoom = Room.Text;           

            theManager.insertMeeting(NewGroupid, newDay, NewStart, NewEnd, newRoom);
            //theManager.CurrentMeeting.MeetingId = newGroupId;
            theManager.ReloadAll();


            MainWindow objMainWindow = (MainWindow)Window.GetWindow(this);

            objMainWindow.Content = new Home();
        }

        private void CancelMeeting_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = (MainWindow)Window.GetWindow(this);

            objMainWindow.Content = new Home();
        }
    }
}
