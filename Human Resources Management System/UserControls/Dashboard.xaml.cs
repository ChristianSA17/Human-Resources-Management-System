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
using MongoDB.Driver;

namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        private readonly MongoDbConnection _connection;
        public Dashboard()
        {
            InitializeComponent();
            SetCurrentDate();
            _connection = new MongoDbConnection();
            LoadData();

        }

        private void SetCurrentDate()
        {
            // Set the current date on the Calendar
            RealTimeCalendar.SelectedDate = DateTime.Now;
            RealTimeCalendar.DisplayDate = DateTime.Now;
        }

        private void LoadData()
        {
           var usersCollection = _connection.GetUsersCollection();
            List<UsersModel> users = usersCollection.Find(FilterDefinition<UsersModel>.Empty).ToList();
            ListViewUsers.ItemsSource = users;
        }
    }
}
