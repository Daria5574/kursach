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
    public class Theme : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }

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
        [ForeignKey("ID_User")]
        public User User { get; set; }
        public List<Book_Theme>? Book_Theme { get; set; } = new();
        public Theme() { }
        public Theme(string name, int iD_User)
        {
            this.Name = name;
            this.ID_User = iD_User;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
