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
    /// Interaction logic for CreateProfile.xaml
    /// </summary>
    public partial class CreateProfile : Page
    {
        public CreateProfile()
        {
            InitializeComponent();
            Continue.IsEnabled = false;

            Title.Items.Add("Mr");
            Title.Items.Add("Mrs");
            Title.Items.Add("Dr");
            Title.Items.Add("Other");
        }

        private void ConfirmEmail_changed(object sender, TextChangedEventArgs e)
        {
            if (Email.Text.Contains(ConfirmEmail.Text)) { Continue.IsEnabled = true; }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            //call UpdateStudent()


            //move to group selection
            //commit changes to database from information displayed in Title, Hobart/Launceston, Email and ProfilePicture
            //continue to joining or creating a group
            MainWindow objMainWindow = (MainWindow)Window.GetWindow(this);

            objMainWindow.Content = new SelectGroup();
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            //open file browser
            //display image in ProfilePicture
        }
    }
}
