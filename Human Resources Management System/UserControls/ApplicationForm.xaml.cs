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
    /// Interaction logic for ApplicationForm.xaml
    /// </summary>
    public partial class ApplicationForm : UserControl
    {
        public ApplicationForm()
        {
            InitializeComponent();
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
                DateTextBox.Text = Calendar.SelectedDate.Value.ToString("yyyy-MM-dd");
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
    }
}
