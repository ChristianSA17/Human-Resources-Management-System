using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<string> Notes { get; set; } = new ObservableCollection<string>();
        public Dashboard()
        {
            InitializeComponent();
            SetCurrentDate();
            _connection = new MongoDbConnection();
            LoadData();
            NotesListBox.ItemsSource = Notes;

            // Populate the Notes initially and schedule updates
            UpdateNotes();

        }

        private void SetCurrentDate()
        {
            // Set the current date on the Calendar
            RealTimeCalendar.SelectedDate = DateTime.Now;
            RealTimeCalendar.DisplayDate = DateTime.Now;
        }

        private void LoadData()
        {
           var peoplesCollection = _connection.GetPeoplesCollection();
            List<PeoplesModel> users = peoplesCollection.Find(FilterDefinition<PeoplesModel>.Empty).ToList();
            ListViewUsers.ItemsSource = users;
        }
        private void UpdateNotes()
        {
            Notes.Clear();

            // Example events - Replace this with real event-fetching logic
            var today = DateTime.Now;
            var events = new[]
            {
                new { Date = new DateTime(today.Year, 2, 14), Name = "Valentine's Day" },
                new { Date = new DateTime(today.Year, 12, 25), Name = "Christmas" },
            };

            foreach (var ev in events)
            {
                if (ev.Date >= today)
                {
                    Notes.Add($"{ev.Name} is on {ev.Date:MMMM dd}");
                }
            }

            // Set to automatically refresh notes every day
            var nextUpdate = DateTime.Today.AddDays(1).Subtract(DateTime.Now);
            var timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = nextUpdate,
                IsEnabled = true,
            };
            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                UpdateNotes();
            };
        }

    }
}
