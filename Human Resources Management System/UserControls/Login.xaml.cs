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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        /*Function to ng hyperlink na para mapunta sa Signup. Bali trinitrigger nito yung function na nasa LoginAndSignup window para mahide yung login na usercontrol at mashow yung signup na usercontrol*/
        private void SignupHyperlink_Click(object sender, RoutedEventArgs e)
        {
            var loginandsignup = (LoginAndSignup)Application.Current.MainWindow;
            loginandsignup.SignupHyperlink();
        }

        /*Function para mabuksan yung HomeDesign na window (hindi pa final, for experimental lang para makita yung design */ 
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {   
            HomeDesign homeDesign = new HomeDesign();
            homeDesign.Show();
            
        }
    }
}
