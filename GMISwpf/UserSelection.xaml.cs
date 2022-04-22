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
    /// Interaction logic for UserSelection.xaml
    /// </summary>
    public partial class UserSelection : Page
    {
        private DataManager theManager;

        public UserSelection ()
        {
            InitializeComponent ();
            goButton.IsEnabled = false;

            theManager = (DataManager)Application.Current.FindResource ("datamanager");
        }

        private void Button_Click (object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = (MainWindow)Window.GetWindow (this);

            theManager.CurrentStudent = (Student)studentList.SelectedItem;

            objMainWindow.Content = new Home ();
        }

        private void studentList_SelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            goButton.IsEnabled = true;
        }
    }
}
