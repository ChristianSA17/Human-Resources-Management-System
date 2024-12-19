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
        
        private ObservableCollection<Item> Items { get; set; }
        private ICollectionView CollectionView { get; set; }
        public Payroll()
        {
            InitializeComponent();
            Items = new ObservableCollection<Item>
            {
                new Item { Name = "Ben" },
                new Item { Name = "Bill" },
                new Item { Name = "Cherry" },
                new Item { Name = "Dante" },
                new Item { Name = "Luffy" },
            };

            // Set up CollectionView for filtering
            CollectionView = CollectionViewSource.GetDefaultView(Items);
            ItemListView.ItemsSource = CollectionView;
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CollectionView == null || SearchTextBox == null)
                return; // Avoid null reference errors

            var searchText = SearchTextBox.Text?.ToLower() ?? string.Empty;

            // Apply the filter to the CollectionView
            CollectionView.Filter = item =>
            {
                if (item is Item currentItem && !string.IsNullOrEmpty(currentItem.Name))
                {
                    return currentItem.Name.ToLower().Contains(searchText);
                }
                return false;
            };

            CollectionView.Refresh();
        }
    }
    public class Item
    {
        public string Name { get; set; }
    }
}
