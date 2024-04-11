using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace kursach.Model
{
    public class User : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string fname;
        public string FName
        {
            get { return fname; }
            set
            {
                fname = value;
                OnPropertyChanged("FName");
            }
        }
        private string? lname;
        public string? LName
        {
            get { return lname; }
            set
            {
                lname = value;
                OnPropertyChanged("LName");
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        public List<Book>? Books { get; set; } = new();
        public List<Author>? Authors { get; set; } = new();

        public List<Bookshelf>? Bookshelfs { get; set; } = new();
        public User() { }
        public User(int id, string fName,
        string lName, string email, string password)
        {
            this.Id = id;
            this.FName = fName;
            this.LName = lName;
            this.Email = email;
            this.Password = password;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
