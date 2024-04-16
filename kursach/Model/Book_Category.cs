using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace kursach.Model
{
    public class Book_Category : INotifyPropertyChanged
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
        public List<Book_Category>? Book_Categories { get; set; } = new();

        private int? iD_Category;
        public int? ID_Category
        {
            get { return iD_Category; }
            set
            {
                iD_Category = value;
                OnPropertyChanged("ID_Category");
            }
        }
        public Category Category { get; set; } = new();

        public Book_Category() { }
        public Book_Category(int iD_Book,
        int iD_Category)
        {
            this.ID_Book = iD_Book;
            this.ID_Category = iD_Category;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
