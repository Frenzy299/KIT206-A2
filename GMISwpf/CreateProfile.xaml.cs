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
using Microsoft.Win32;

namespace GMISwpf
{
    /// <summary>
    /// Interaction logic for CreateProfile.xaml
    /// </summary>
    public partial class CreateProfile : Page
    {
        private DataManager theManager;

        public CreateProfile()
        {
            InitializeComponent();
            theManager = (DataManager)Application.Current.FindResource ("datamanager");
            //Continue.IsEnabled = false;
            
            Title.Items.Add("Mr");
            Title.Items.Add("Mrs");
            Title.Items.Add("Dr");
            Title.Items.Add("Other");

            Campus.Items.Add ("Hobart");
            Campus.Items.Add ("Launceston");

            Category.Items.Add ("Bachelors");
            Category.Items.Add ("Masters");
        }

        

        private void ConfirmEmail_changed(object sender, TextChangedEventArgs e)
        {
            //if (Email.Text.Contains(ConfirmEmail.Text)) { Continue.IsEnabled = true; }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            //call UpdateStudent()
            int studentToUpdate = theManager.CurrentStudent.StudentId;
            string newTitle = Title.Text;
            string newCampus = Campus.Text;
            string newPhone = Phone.Text;
            string newEmail = Email.Text;
            string newCategory = Category.Text;

            // if details arent valid
            if (Email.Text != ConfirmEmail.Text)
            {
                MessageBox.Show ("FAIL MESSAGE");
            }
            else
            {
                theManager.UpdateStudent (studentToUpdate, newTitle, newCampus, newPhone, newEmail, newCategory);
                
                //move to group selection
                MainWindow objMainWindow = (MainWindow)Window.GetWindow (this);

                objMainWindow.Content = new SelectGroup ();
            }
            
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            //open file browser
            //display image in ProfilePicture
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri imageUri = new Uri(openFileDialog.FileName);
                ProfilePicture.Source = new BitmapImage(imageUri);
            }
        }

        private void Campus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CampusLabel.Content = Campus.Text;
        }

        private void Title_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NameLabel.Content = Title.Text + " " + theManager.CurrentStudent.GivenName + " " + theManager.CurrentStudent.FamilyName;
        }
    }
}
