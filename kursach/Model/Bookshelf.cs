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
    public class Bookshelf : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }

        private int iD_User;
        public int ID_User
        {
            get { return iD_User; }
            set
            {
                iD_User = value;
                OnPropertyChanged(nameof(ID_User));
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string? color_Code;
        public string? Color_Code
        {
            get { return color_Code; }
            set
            {
                color_Code = value;
                OnPropertyChanged(nameof(Color_Code));
            }
        }
        [ForeignKey("ID_User")]
        public User User { get; set; }
        public List<Book_Bookshelf>? Book_Bookshelf { get; set; } = new();
        public Bookshelf() { }
        public Bookshelf(int id_user, string name, string color_Code)
        {
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