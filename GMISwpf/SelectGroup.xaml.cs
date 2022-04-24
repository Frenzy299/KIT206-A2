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
    /// Interaction logic for SelectGroup.xaml
    /// </summary>
    public partial class SelectGroup : Page
    {
        private DataManager theManager;
        
        public SelectGroup()
        {
            InitializeComponent();
            theManager = (DataManager)Application.Current.FindResource ("datamanager");
            Join.IsEnabled = false;
        }

        private void Join_Click(object sender, RoutedEventArgs e)
        {
            //change current student's group to the one selected in GroupList
            int id = theManager.CurrentStudent.StudentId;
            Group newGroup = (Group)GroupList.SelectedItem;

            int newGroupId = newGroup.GroupId;

            theManager.UpdateStudentGroup (id, newGroupId);
            theManager.CurrentStudent.GroupId = newGroupId;
            theManager.ReloadAll ();

            //return home
            MainWindow objMainWindow = (MainWindow)Window.GetWindow(this);

            objMainWindow.Content = new Home();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            int id = theManager.CurrentStudent.StudentId;
            string groupName = GroupName.Text;

            //add group to database with name as text displayed in GroupName
            theManager.insertGroup(groupName);

            //change the group of current student
            theManager.FilterGroupsByName(groupName);
            int groupID = theManager.FilteredGroups[0].GroupId;
            theManager.UpdateStudentGroup(id, groupID);
            
            //return home
            MainWindow objMainWindow = (MainWindow)Window.GetWindow(this);

            objMainWindow.Content = new Home();
        }

        private void GroupList_SelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            Join.IsEnabled = true;
        }
    }
}
