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

            if (theManager.CurrentStudent != null) studentNameLabel.Content = theManager.CurrentStudent.GivenName;

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

        private void ListBox_SelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            // Pass selected student to the StackPanel called detailsPanel
            //detailsPanel.DataContext = nameList.SelectedItem;
        }

        private void meetingList_SelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            meetingPanel.DataContext = meetingList.SelectedItem;
            
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
            theManager.insertMeeting(AllMeeting);
        }

        private void EditMeeting_Click(object sender, RoutedEventArgs e)
        {
            theManager.CurrentMeeting = (Meeting)meetingList.SelectedItem;
            MainWindow objMainWindow = (MainWindow)Window.GetWindow(this);

            objMainWindow.Content = new EditMeeting();
        }
    }
}
