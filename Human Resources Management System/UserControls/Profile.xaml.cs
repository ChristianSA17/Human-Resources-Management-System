using Microsoft.Win32;
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

namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Load the selected image into the Image control
                    var imagePath = openFileDialog.FileName;
                    ProfileImage.Source = new BitmapImage(new Uri(imagePath));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            FirstNameTextBox.IsReadOnly = false;
            LastNameTextbox.IsReadOnly = false;
            MiddleNameTextBox.IsReadOnly = false;
            ContactNoTextBox.IsReadOnly = false;
            EmailTextBox.IsReadOnly = false;

            EditButton.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Visible;


        }

        private void CancelEditButton_Click(object sender, RoutedEventArgs e)
        {
            FirstNameTextBox.IsReadOnly = true;
            LastNameTextbox.IsReadOnly = true;
            MiddleNameTextBox.IsReadOnly = true;
            ContactNoTextBox.IsReadOnly = true;
            EmailTextBox.IsReadOnly = true;

            EditButton.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Collapsed;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var firstName = FirstNameTextBox.Text;
            var lastName = LastNameTextbox.Text;
            var middleName = MiddleNameTextBox.Text;
            var contactNo = ContactNoTextBox.Text;
            var email = EmailTextBox.Text;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(middleName) || string.IsNullOrWhiteSpace(contactNo) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Fill up all the fields.");
                return;
            }
        }
    }
}
