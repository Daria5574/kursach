using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace kursach.Model
{
    public class User : INotifyPropertyChanged
    {
        [Key]
        public int ID_User { get; set; }

        private string? fname;
        public string? FName
        {
            get { return fname; }
            set
            {
                fname = value;
                OnPropertyChanged(nameof(FName));
            }
        }
        private string? lname;
        public string? LName
        {
            get { return lname; }
            set
            {
                lname = value;
                OnPropertyChanged(nameof(LName));
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public List<Book>? Book { get; set; } = new();
        public List<Author>? Author { get; set; } = new();

        public List<Bookshelf>? Bookshelf { get; set; } = new();
        public User() { }
        public User(string? fName,
        string? lName, string email, string password)
        {
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
