﻿using System;
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
    public class Book_Bookshelf : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }

        private int? iD_Book;
        public int? ID_Book
        {
            get { return iD_Book; }
            set
            {
                iD_Book = value;
                OnPropertyChanged(nameof(ID_Book));
            }
        }
        [ForeignKey("ID_Book")]
        public Book Book { get; set; }

        private int? iD_Bookshelf;
        public int? ID_Bookshelf
        {
            get { return iD_Bookshelf; }
            set
            {
                iD_Bookshelf = value;
                OnPropertyChanged(nameof(ID_Bookshelf));
            }
        }
        [ForeignKey("ID_Bookshelf")]
        public Bookshelf Bookshelf { get; set; }

        public Book_Bookshelf() { }
        public Book_Bookshelf(int iD_Book,
        int iD_Bookshelf)
        {
            this.ID_Book = iD_Book;
            this.ID_Bookshelf = iD_Bookshelf;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
