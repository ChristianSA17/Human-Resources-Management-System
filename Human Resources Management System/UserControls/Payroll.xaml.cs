using SharpVectors.Dom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using SharpVectors.Dom;

namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for Payroll.xaml
    /// </summary>
    
    public partial class Payroll : UserControl
    {
        
        
        public Payroll()
        {
            InitializeComponent();
           
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
    }
    public class Item
    {
        public string Name { get; set; }
    }
}
