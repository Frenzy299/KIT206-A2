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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private DataManager theManager;

        public Home ()
        {
            InitializeComponent ();
            theManager = (DataManager)Application.Current.FindResource ("datamanager");
            GroupNameLabel.Content = theManager.GetGroupNameFromId(theManager.CurrentStudent.GroupId);
            memberDetailsButton.IsEnabled = false;

            if (theManager.CurrentStudent != null) studentNameLabel.Content = theManager.CurrentStudent.GivenName + " " + theManager.CurrentStudent.FamilyName;

            theManager.FilterStudentsByGroup (theManager.CurrentStudent.GroupId);
            theManager.FilterMeetingsByGroup (theManager.CurrentStudent.GroupId);
            theManager.FilterClassesByGroup (theManager.CurrentStudent.GroupId);

            classPanel.DataContext = theManager.FilteredClasses[0];
        }

        // Only a test
        private void BtnRemoveStudent (object sender, RoutedEventArgs e)
        {
            theManager.FilterStudentsByGroup (1);


        }

        // 'View member details' button is only available when a member is selected.
        // Attempting to view a member with no member selected would cause a crash
        private void ListBox_SelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            memberDetailsButton.IsEnabled = true;
            
            // Pass selected student to the StackPanel called detailsPanel
            //detailsPanel.DataContext = nameList.SelectedItem;
        }

        private void meetingList_SelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            meetingPanel.DataContext = meetingList.SelectedItem;
            theManager.CurrentMeeting = (Meeting)meetingList.SelectedItem;

        }

        private void ChangeGroup_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = (MainWindow)Window.GetWindow(this);

            objMainWindow.Content = new ChangeGroup();
        }

        private void EditGroup_Click(object sender, RoutedEventArgs e)
        {
            
            
            MainWindow objMainWindow = (MainWindow)Window.GetWindow(this);

            objMainWindow.Content = new EditGroup();
        }

        private void AddMeeting_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = (MainWindow)Window.GetWindow(this);

            objMainWindow.Content = new AddMeeting();
        }

        private void EditMeeting_Click(object sender, RoutedEventArgs e)
        {
            theManager.CurrentMeeting = (Meeting)meetingList.SelectedItem;
            MainWindow objMainWindow = (MainWindow)Window.GetWindow(this);

            objMainWindow.Content = new EditMeeting();
        }

        // Display a message box with extra information about your group members
        private void memberDetailsButton_Click (object sender, RoutedEventArgs e)
        {
            Student theStudent = (Student)nameList.SelectedItem;

            string bodyText = String.Format ("{0} {1}\nPH: {2}\nEmail: {3}", theStudent.GivenName, theStudent.FamilyName, theStudent.Phone, theStudent.Email);


            MessageBox.Show (bodyText, "Member details");
        }

        private void LogOut_Click (object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = (MainWindow)Window.GetWindow (this);

            objMainWindow.Content = new UserSelection ();
        }
    }
}
