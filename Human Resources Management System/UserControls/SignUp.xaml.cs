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

        /*Function to ng hyperlink na para mapunta sa login. Bali trinitrigger nito yung function na nasa LoginAndSignup window para mahide yung signup na usercontrol at mashow yung login na usercontrol*/
        private void LoginHyperlink_Click(object sender, RoutedEventArgs e)
        {
          var loginandsignup = (LoginAndSignup)Application.Current.MainWindow;
            loginandsignup.LoginHyperlink();
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                var firstName = SignUpFirstName.Text;
                var lastName = SignUpLastName.Text;
                var middleName = SignUpMiddleName.Text;
                var contactNo = SignUpContactNo.Text;
                var email = SignUpEmail.Text;
                var username = SignupUsername.Text;
                var password = SignupPassword.Password;
                var Cpassword = SignupCPassword.Password;

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
                    MessageBox.Show("The username already exist.");
                    return;

                }

                var hashedPassword = HashPassword(password);

                var newUser = new UsersModel {FirstName = firstName, LastName = lastName, MiddleName = middleName, ContactNo = contactNo, Email = email, Username = username, Password = hashedPassword};
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
    }
}
