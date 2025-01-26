using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Human_Resources_Management_System
{

    public class EditViewModel : INotifyPropertyChanged
    {
        private PeoplesModel _item;
        public PeoplesModel Item
        {
            get { return _item; }
            set { _item = value; OnPropertyChanged(nameof(Item)); }
        }

        public bool SaveChanges()
        {
            try
            {
                // Perform save logic here (e.g., update the database or collection)
                // Example:
                // database.Update(Item);

                return true; // Return true if the save operation succeeds
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine($"Error saving changes: {ex.Message}");
                return false; // Return false if the save operation fails
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

  

