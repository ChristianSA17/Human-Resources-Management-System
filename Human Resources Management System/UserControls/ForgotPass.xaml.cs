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
    /// Interaction logic for ForgotPass.xaml
    /// </summary>
    public partial class ForgotPass : UserControl
    {
        public ForgotPass()
        {
            InitializeComponent();
        }

        private void BackForgotPass_Click(object sender, RoutedEventArgs e)
        {
            var loginandsignup = (LoginAndSignup)Application.Current.MainWindow;
            loginandsignup.LoginHyperlink();
        }

        private void SaveForgotPass_Click(object sender, RoutedEventArgs e)
        {
            var loginandsignup = (LoginAndSignup)Application.Current.MainWindow;
            loginandsignup.LoginHyperlink();
        }

        private void Send_OTPBtn(object sender, RoutedEventArgs e)
        {

        }
    }
}
