using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
using System.Security.Cryptography;
using System.Text.RegularExpressions;


namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : UserControl
    {
        private readonly MongoDbConnection _connection;
        public SignUp()
        {
            InitializeComponent();
            _connection = new MongoDbConnection();
        }

        //Set what inputs can only be added in the username textboxes
        private void Character_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allow only alphanumeric characters and some symbols
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-";
            if (!allowedChars.Contains(e.Text))
            {
                e.Handled = true;  // Reject the input
            }
        }

        private void Username_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allow only alphanumeric characters and some symbols
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.-_";
            if (!allowedChars.Contains(e.Text))
            {
                e.Handled = true;  // Reject the input
            }
        }

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allow only alphanumeric characters and some symbols
            string allowedChars = "1234567890";
            if (!allowedChars.Contains(e.Text))
            {
                e.Handled = true;  // Reject the input
            }
        }

        /*Function to ng hyperlink na para mapunta sa login. Bali trinitrigger nito yung function na nasa LoginAndSignup window para mahide yung signup na usercontrol at mashow yung login na usercontrol*/
        private void LoginHyperlink_Click(object sender, RoutedEventArgs e)
        {
          var loginandsignup = (LoginAndSignup)Application.Current.MainWindow;
            loginandsignup.LoginHyperlink();
        }

        // Function to validate the email
        private bool IsValidEmail(string email)
        {
            // Check if the email is empty
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            // Regular Expression for Email Format Validation
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                return false;
            }

            // Check if the email length is less than or equal to 100 characters
            if (email.Length > 100)
            {
                return false;
            }

            return true;
        }

        // Function to validate the contact number
        private bool IsValidContactNumber(string contactNumber)
        {
            // Check if the contact number is empty
            if (string.IsNullOrWhiteSpace(contactNumber))
            {
                return false;
            }

            // Regular Expression for US Phone Number Format Validation (e.g., (123) 456-7890 or 123-456-7890)
            string contactNumberPattern = @"^09\d{9}$"
;

            // Validate against the regular expression
            if (!Regex.IsMatch(contactNumber, contactNumberPattern))
            {
                return false;
            }

            // Optional: Check the length of the phone number (for U.S., should be 10 digits, or 11 including country code)
            if (contactNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Length < 11)
            {
                return false;
            }

            return true;
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                var firstName = SignUpFirstName.Text;
                var lastName = SignUpLastName.Text;
                var middleName = SignUpMiddleName.Text;
                var contactNo = SignUpContactNo.Text;
                var email = SignUpEmail.Text.Trim();
                var username = SignupUsername.Text;
                var password = SignupPassword.Password;
                var Cpassword = SignupCPassword.Password;

                // Validate the email
                if (!IsValidEmail(email))
                {
                    // Display error message
                    EmailErrorMessage.Text = "Please enter a valid email address.";
                    EmailErrorMessage.Visibility = Visibility.Visible;
                }
               

                // Validate the contact number
                if (!IsValidContactNumber(contactNo))
                {
                    // Display error message
                    ContactNumberErrorMessage.Text = "Please enter a valid contact number.";
                    ContactNumberErrorMessage.Visibility = Visibility.Visible;
                }
              
                    
          

                if (new[] {firstName, lastName, middleName, contactNo, email, username, password, Cpassword }.Any(string.IsNullOrWhiteSpace))
                {
                    MessageBox.Show("Fill up all the fields.");
                    return;
                }

                if (password != Cpassword)
                {
                    MessageBox.Show("Passwords do not match.");
                    return;
                }


                var userCollection = _connection.GetUsersCollection();

                // Checks if user already exists.
                if (userCollection.AsQueryable().Any(u => u.Username == username))
                {
                    MessageBox.Show("Username already exist.");
                    return;

                }

                // Checks if email already exists.
                if (userCollection.AsQueryable().Any(u => u.Email == email))
                {
                    MessageBox.Show("Email already exist.");
                    return;

                }

                var hashedPassword = HashPassword(password);

                var newUser = new UsersModel {FirstName = firstName, LastName = lastName, MiddleName = middleName, ContactNo = contactNo, Email = email, Username = username, Password = hashedPassword, ApprovalStatus = "Pending"};
                userCollection.InsertOne(newUser);

                var loginandsignup = (LoginAndSignup)Application.Current.MainWindow;
                loginandsignup.LoginHyperlink();




            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}");
                return;
            }
        
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private void SignUpEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

            var email = SignUpEmail.Text.Trim();
            // Hide the error message while the user is typing
            EmailErrorMessage.Visibility = Visibility.Collapsed;

            // Real-time validation can be done here if needed
            if (!IsValidEmail(email))
            {
                EmailErrorMessage.Text = "Invalid email format.";
                EmailErrorMessage.Visibility = Visibility.Visible;
            }
        }
    }
}
