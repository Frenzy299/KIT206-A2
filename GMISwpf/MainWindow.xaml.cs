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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataManager theManager;

        public MainWindow ()
        {
            InitializeComponent ();

            // Reference to DataManager created in App.xaml
            theManager = (DataManager)Application.Current.FindResource ("datamanager");
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
    }
}
