using Human_Resources_Management_System.UserControls;
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
using System.Windows.Shapes;
using MongoDB.Driver;

namespace Human_Resources_Management_System
{
    /// <summary>
    /// Interaction logic for HomeDesign.xaml
    /// </summary>
    public partial class HomeDesign : Window
    {

        private readonly MongoDbConnection _connection;

        public HomeDesign()
        {
            InitializeComponent();
            _connection = new MongoDbConnection();
            ContentDisplay.Content = new Dashboard();
            DashboardBtn.MouseEnter += HoverDashboardBtn;
            DashboardBtn.MouseLeave += HoverDashboardBtn_Leave;
            ApplicationFormBtn.MouseEnter += HoverAppFormBtn;
            ApplicationFormBtn.MouseLeave += HoverAppFormBtn_Leave;
            ProfileBtn.MouseEnter += HoverProfileBtn;
            ProfileBtn.MouseLeave += HoverProfileBtn_Leave;
            PayrollBtn.MouseEnter += HoverPrBtn;
            PayrollBtn.MouseLeave += HoverPrBtn_Leave;
            SupportBtn.MouseEnter += HoverSupportBtn;
            SupportBtn.MouseLeave += HoverSupportBtn_Leave;
            ExitBtn.MouseEnter += HoverExitBtn;
            ExitBtn.MouseLeave += HoverExitBtn_Leave;
        }

        
        //ito yung para mapakita yung user control window sa grid row 1. pag mag seselect ka ng section sa grid row 0//
        private void HoverDashboardBtn(object sender, RoutedEventArgs e)
        {
            
            DashboardBtn.Foreground = Brushes.Black;
            

        }
        private void HoverDashboardBtn_Leave(object sender, RoutedEventArgs e)
        {
            DashboardBtn.Background = (Brush)new BrushConverter().ConvertFromString("#343030");
            DashboardBtn.Foreground = Brushes.White;

        }
        private void HoverAppFormBtn(object sender, RoutedEventArgs e)
        {
            ApplicationFormBtn.Foreground = Brushes.Black;
        }
        private void HoverAppFormBtn_Leave(object sender, RoutedEventArgs e)
        {
            ApplicationFormBtn.Background = (Brush)new BrushConverter().ConvertFromString("#343030");
            ApplicationFormBtn.Foreground = Brushes.White;
        }
        private void HoverProfileBtn(object sender, RoutedEventArgs e)
        {
            ProfileBtn.Foreground = Brushes.Black;
        }
        private void HoverProfileBtn_Leave(object sender, RoutedEventArgs e)
        {
            ProfileBtn.Background = (Brush)new BrushConverter().ConvertFromString("#343030");
            ProfileBtn.Foreground = Brushes.White;
        }
        private void HoverPrBtn(object sender, RoutedEventArgs e)
        {
            PayrollBtn.Foreground = Brushes.Black;
        }
        private void HoverPrBtn_Leave(object sender, RoutedEventArgs e)
        {
            PayrollBtn.Background = (Brush)new BrushConverter().ConvertFromString("#343030");
            PayrollBtn.Foreground = Brushes.White;
        }
        private void HoverSupportBtn(object sender, RoutedEventArgs e)
        {
            SupportBtn.Foreground = Brushes.Black;
        }
        private void HoverSupportBtn_Leave(object sender, RoutedEventArgs e)
        {
            SupportBtn.Background = (Brush)new BrushConverter().ConvertFromString("#343030");
            SupportBtn.Foreground = Brushes.White;
        }
        private void HoverExitBtn(object sender, RoutedEventArgs e)
        {
            ExitBtn.Foreground = Brushes.Black;
        }
        private void HoverExitBtn_Leave(object sender, RoutedEventArgs e)
        {
            ExitBtn.Background = (Brush)new BrushConverter().ConvertFromString("#343030");
            ExitBtn.Foreground = Brushes.White;
        }


        private void DashboardBtn_Click(object sender, RoutedEventArgs e)
        {
            
                ContentDisplay.Content = new Dashboard();

        }


        private void ApplicationFormBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentDisplay.Content = new ApplicationForm();
        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentDisplay.Content = new Profile(UsernameText.Text);
        }

        private void SupportBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentDisplay.Content = new SupportContacts();
        }

        private void SignOutBtn_Click(object sender, RoutedEventArgs e)
        {

            Window parentWindow = Application.Current.MainWindow;
            MessageBoxResult boxResult = System.Windows.MessageBox.Show("Are you sure you want to Sign Out?", "Close Window?", System.Windows.MessageBoxButton.YesNo);
            if(boxResult == System.Windows.MessageBoxResult.Yes)
            {
                parentWindow.Show();
                this.Close();

            }
        }

        private void PayrollBtn_Click(object sender, RoutedEventArgs e)
        {

            ContentDisplay.Content = new Payroll();
        }
     

        private void SignOut()
        {
            // Example sign-out logic
            // Clear user session, tokens, or notify the server
            MessageBox.Show("You have been signed out.", "Sign Out", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
