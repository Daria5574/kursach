using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace kursach.Model
{

    public class Bookshelf : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int iD_User;
        public int ID_User
        {
            get { return iD_User; }
            set
            {
                iD_User = value;
                OnPropertyChanged("ID_User");
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string? color_Code;
        public string? Color_Code
        {
            get { return color_Code; }
            set
            {
                color_Code = value;
                OnPropertyChanged("Color_Code");
            }
        }
        public User User { get; set; } = new();
        public List<Book_Bookshelf>? Book_Bookshelfs { get; set; } = new();
        public Bookshelf() { }
        public Bookshelf(int id, int id_user, string name,
        string color_Code)
        {
            this.Id = id;
            this.ID_User = id_user;
            this.Name = name;
            this.Color_Code = color_Code;

        }
        public Bookshelf ShallowCopy()
        {
            return (Bookshelf)this.MemberwiseClone();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}