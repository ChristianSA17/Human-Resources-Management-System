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

        private async void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                var newUser = new UsersModel
                {
                   
                    Username = Username.Text,
                    Password = Password.Text,
                    
                };

                await _connection.AddUserAsync(newUser);

                MessageBox.Show("User added successfully!");
                var loginandsignup = (LoginAndSignup)Application.Current.MainWindow;
                loginandsignup.LoginHyperlink();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}");
            }
        
        }
    }
}
