using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace kursach.Model
{
    public class Book_Theme : INotifyPropertyChanged
    {

        public int Id { get; set; }

        private int? iD_Book;
        public int? ID_Book
        {
            get { return iD_Book; }
            set
            {
                iD_Book = value;
                OnPropertyChanged("ID_Book");
            }
        }
        public Book Book { get; set; } = new();

        private int? iD_Theme;
        public int? ID_Theme
        {
            get { return iD_Theme; }
            set
            {
                iD_Theme = value;
                OnPropertyChanged("ID_Theme");
            }
        }
        public Theme Theme { get; set; } = new();

        public Book_Theme() { }
        public Book_Theme(int iD_Book, int iD_Theme)
        {
            this.ID_Book = iD_Book;
            this.ID_Theme = iD_Theme;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
