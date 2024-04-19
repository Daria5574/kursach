using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel;
using kursach.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using kursach.Helper;
using kursach.View;

namespace kursach.ViewModel
{
    public class WindowAddBookViewModel : INotifyPropertyChanged
    {
        DatabaseContext db = new DatabaseContext();

        public ObservableCollection<string> Authors { get; set; }

        public string SelectedAuthor
        {
            get => _selectedAuthor;
            set
            {
                _selectedAuthor = value;
                OnPropertyChanged(nameof(SelectedAuthor));
            }

        }

        private string _selectedAuthor;

        public WindowAddBookViewModel()
        {
            var authors = db.author
                .Where(a => a.ID_User == App.currentUser.ID_User)
                .Select(a => a.FName + " " + a.LName)
                .ToList();
            Authors = new ObservableCollection<string>(authors);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

