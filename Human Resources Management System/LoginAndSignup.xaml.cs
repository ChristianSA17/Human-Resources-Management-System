using Human_Resources_Management_System.UserControls;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Human_Resources_Management_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginAndSignup : Window
    {
        public LoginAndSignup()
        {
            InitializeComponent();
        }

        /* Function para mashow yung login usercontrol at mahide yung signup user control, then vise versa sa function na nasa baba*/
        public void LoginHyperlink() 
        {
            LoginUserControl.Visibility = Visibility.Visible;
            SignupUserControl.Visibility = Visibility.Collapsed;
        }

        public void SignupHyperlink()
        {
            LoginUserControl.Visibility = Visibility.Collapsed;
            SignupUserControl.Visibility = Visibility.Visible;
           
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}