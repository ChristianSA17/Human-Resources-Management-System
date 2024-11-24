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

namespace Human_Resources_Management_System
{
    /// <summary>
    /// Interaction logic for HomeDesign.xaml
    /// </summary>
    public partial class HomeDesign : Window
    {
        public HomeDesign()
        {
            InitializeComponent();
        }

        
        //ito yung para mapakita yung user control window sa grid row 1. pag mag seselect ka ng section sa grid row 0//
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
            ContentDisplay.Content = new Profile();
        }

        private void SupportBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentDisplay.Content = new SupportContacts();
        }

        
    }
}
