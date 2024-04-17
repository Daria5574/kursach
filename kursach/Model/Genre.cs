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
    public class Genre : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }

        private int iD_Category;
        public int ID_Category
        {
            get { return iD_Category; }
            set
            {
                iD_Category = value;
                OnPropertyChanged("ID_Category");
            }
        }
        [ForeignKey("ID_Category")]
        public Category Category { get; set; }

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
        public Genre() { }
        public Genre(int id_category, string name)
        {
            this.ID_Category = id_category;
            this.Name = name;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
