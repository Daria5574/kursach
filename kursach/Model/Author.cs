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
    public class Author : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }

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
        private string lname;
        public string LName
        {
            get { return lname; }
            set
            {
                lname = value;
                OnPropertyChanged(nameof(LName));
            }
        }


        private string? description;
        public string? Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        private int? iD_User;
        public int? ID_User
        {
            get { return iD_User; }
            set
            {
                iD_User = value;
                OnPropertyChanged(nameof(ID_User));
            }
        }
        [ForeignKey("ID_User")]
        public User User { get; set; }

        public Author() { }
        public Author( string? fName,
        string lName, string? description, int iD_User)
        {
            this.FName = fName;
            this.LName = lName;
            this.Description = description;
            this.ID_User = iD_User;
        }
        public List<Book>? Book { get; set; } = new();
        public Author ShallowCopy()
        {
            return (Author)this.MemberwiseClone();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
