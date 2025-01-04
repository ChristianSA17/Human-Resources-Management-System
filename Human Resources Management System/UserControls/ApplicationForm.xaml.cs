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
using MongoDB.Driver;

namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for ApplicationForm.xaml
    /// </summary>
    public partial class ApplicationForm : UserControl
    {
        private readonly MongoDbConnection _connection;
        public ApplicationForm()
        {
            InitializeComponent();
            _connection = new MongoDbConnection();
            Signature signature = new Signature();
            
        }

        private void ShowCalendar_Click(object sender, RoutedEventArgs e)
        {
            CalendarPopup.IsOpen = !CalendarPopup.IsOpen;
        }

        private void ShowDateHired_Click(object sender, RoutedEventArgs e)
        {

            HiredDatePopup.IsOpen = !HiredDatePopup.IsOpen;

        }

        // Set the selected date in the TextBox when a date is selected
        private void Calendar_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Calendar.SelectedDate.HasValue)
            {
                BirthDateTextBox.Text = Calendar.SelectedDate.Value.ToString("yyyy-MM-dd");
            }

            // Close the popup when a date is selected
            CalendarPopup.IsOpen = false;
        }

        private void Hired_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HiredDate.SelectedDate.HasValue)
            {
                HiredDateTextBox.Text = HiredDate.SelectedDate.Value.ToString("yyyy-MM-dd");
            }

            // Close the popup when a date is selected
            HiredDatePopup.IsOpen = false;
        }

        private void OpenEditForm_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void OpenSignature_Click(object sender, RoutedEventArgs e)
        {
            Signature signature = new Signature();
            Window hostWindow = new Window
            {
                Content = signature,
                Width = 400,
                Height = 300,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            hostWindow.Show(); // Display the UserControl within a window
        }

        private void FormUploadButton_Click(object sender, RoutedEventArgs e)
        {
            // Open a file dialog to select an image
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Load the selected image into the Image control
                BitmapImage bitmap = new BitmapImage(new System.Uri(openFileDialog.FileName));

                // Set the source of the Image control to the loaded image
                UploadedImage.Source = bitmap;
            }
        }

        private void AuthoritySig_Click(object sender, RoutedEventArgs e)
        {
            Signature signature = new Signature();
            Window hostWindow = new Window
            {
                Content = signature,
                Width = 400,
                Height = 300,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            hostWindow.Show(); // Display the UserControl within a window
        }

        private void ConfirmApplicationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // collects data from TextBoxes
                var firstName = ApplicantsFirstName.Text;
                var surname = ApplicantsSurname.Text;
                var middleName = ApplicantsMiddleName.Text;
                var age = ApplicantsAge.Text;
                var email = ApplicantsEmail.Text;
                var address = ApplicantsAddress.Text;
                var contactNo = ApplicantsContactNo.Text;
                var shuttleCode = ApplicantsShuttleCode.Text;
                var emergencyName = EmergencyContactName.Text;
                var emergencySurname = EmergencyContactSurname.Text;
                var emergencyMiddleName = EmergencyContactMiddleName.Text;
                var emergencyContact = EmergencyContactNo.Text;
                var emergencyAddress = EmergencyContactAddress.Text;
               
                //string container where the datas from comboxes will be stored and will be used to add data to the database
                string selectedSex = null;
                string selectedRequirements = null;
                string selectedEmergencyContactsSex = null;

                //takes the item from the comboboxes and add to the string containers above
                if (ApplicantsSex.SelectedItem is ComboBoxItem sexItem)
                {
                    selectedSex = sexItem.Content as string;
                }

                if (ApplicantsRequirements.SelectedItem is ComboBoxItem requirementsItem)
                {
                    selectedRequirements = requirementsItem.Content as string;
                }

                if (EmergencyContactSex.SelectedItem is ComboBoxItem emergencyItem)
                {
                    selectedEmergencyContactsSex = emergencyItem.Content as string;
                }

                //checks of the no combobox was selected

                if (string.IsNullOrEmpty(selectedSex))
                {
                    MessageBox.Show("Please select a gender.");
                    return;
                }

                if (string.IsNullOrEmpty(selectedRequirements))
                {
                    MessageBox.Show("Please select a requirements info.");
                    return;
                }


                //checks all the textbox is empty or whitespace
                if (new[] {firstName, surname, middleName, age, email, address, contactNo, shuttleCode, emergencyName, emergencySurname, emergencyMiddleName, emergencyContact, emergencyAddress}.Any(string.IsNullOrWhiteSpace)) {
                    MessageBox.Show("Fill up all the fields.");
                    return;
                }

                var peoplesCollection = _connection.GetPeoplesCollection();

                var newPerson = new PeoplesModel { FirstName = firstName, Surname = surname, MiddleName = middleName, Age = age, Email = email, Address = address, ContactNo = contactNo, ShuttleCode = shuttleCode, ContactsFirstName = emergencyName, ContactsSurname = emergencySurname, ContactsMiddleName = emergencyMiddleName, ContactsNo = emergencyContact, ContactsAddress = emergencyAddress, Sex = selectedSex, Requirements = selectedRequirements, ContactsSex = selectedEmergencyContactsSex};
                peoplesCollection.InsertOne(newPerson);

                MessageBox.Show("Person added successfully!");


            }
            catch (Exception ex) {
                MessageBox.Show($"Error adding user: {ex.Message}");
                return;
            }
        }
    }
}
