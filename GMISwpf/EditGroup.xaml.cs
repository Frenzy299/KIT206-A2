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
    /// Interaction logic for EditGroup.xaml
    /// </summary>
    public partial class EditGroup : Page
    {
        private DataManager theManager;

        public EditGroup()
        {
            InitializeComponent();
            theManager = (DataManager)Application.Current.FindResource("datamanager");

            //Display "Edit (CurrentGroupName)" in GroupNameLabel
            string CurrentStudentName = theManager.GetGroupNameFromId(theManager.CurrentStudent.GroupId);
            GroupNameLabel.Content = String.Format("Edit {0}", CurrentStudentName);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = (MainWindow)Window.GetWindow (this);

            objMainWindow.Content = new Home ();
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            //in database:
            //change group name to the name in GroupName
            int groupID = theManager.CurrentStudent.GroupId;
            string newGroupName = GroupName.Text;
            theManager.UpdateGroup(groupID, newGroupName);

            //return to home
            MainWindow objMainWindow = (MainWindow)Window.GetWindow(this);

            objMainWindow.Content = new Home();
        }
    }
}
