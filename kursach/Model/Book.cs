﻿
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
    public class Book : INotifyPropertyChanged
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

        private int? iD_Author;
        public int? ID_Author
        {
            get { return iD_Author; }
            set
            {
                iD_Author = value;
                OnPropertyChanged(nameof(ID_Author));
            }
        }
        [ForeignKey("ID_Author")]
        public Author Author { get; set; }

        private string the_Path_To_The_File;
        public string The_Path_To_The_File
        {
            get { return the_Path_To_The_File; }
            set
            {
                the_Path_To_The_File = value;
                OnPropertyChanged(nameof(The_Path_To_The_File));
            }
        }

        private string? cover;
        public string? Cover
        {
            get { return cover; }
            set
            {
                cover = value;
                OnPropertyChanged("Cover");
            }
        }

        private int? number_Of_Printed_Pages;
        public int? Number_Of_Printed_Pages
        {
            get { return number_Of_Printed_Pages; }
            set
            {
                number_Of_Printed_Pages = value;
                OnPropertyChanged(nameof(Number_Of_Printed_Pages));
            }
        }
        private int? date_Of_Writing;
        public int? Date_Of_Writing
        {
            get { return date_Of_Writing; }
            set
            {
                date_Of_Writing = value;
                OnPropertyChanged(nameof(Date_Of_Writing));
            }
        }

        private int? the_Year_Of_Publishing;
        public int? The_Year_Of_Publishing
        {
            get { return the_Year_Of_Publishing; }
            set
            {
                the_Year_Of_Publishing = value;
                OnPropertyChanged(nameof(The_Year_Of_Publishing));
            }
        }

        private string? isbn;
        public string? ISBN
        {
            get { return isbn; }
            set
            {
                isbn = value;
                OnPropertyChanged(nameof(ISBN));
            }
        }
        private string? time_To_Read;
        public string? Time_To_Read
        {
            get { return time_To_Read; }
            set
            {
                time_To_Read = value;
                OnPropertyChanged(nameof(Time_To_Read));
            }
        }
        private string? about_The_Book;
        public string? About_The_Book
        {
            get { return about_The_Book; }
            set
            {
                about_The_Book = value;
                OnPropertyChanged(nameof(About_The_Book));
            }
        }
        private string? age_Rating;
        public string? Age_Rating
        {
            get { return age_Rating; }
            set
            {
                age_Rating = value;
                OnPropertyChanged(nameof(Age_Rating));
            }
        }

        private int is_Favorite;
        public int Is_Favorite
        {
            get { return is_Favorite; }
            set
            {
                is_Favorite = value;
                OnPropertyChanged(nameof(Is_Favorite));
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
        public List<Book_Bookshelf>? Book_Bookshelf { get; set; }
        public List<Book_Theme>? Book_Theme { get; set; }
        public List<Book_Category>? Book_Category { get; set; }

        public Book() 
        {
            this.Is_Favorite = 0;
        }
        public Book(string name, int iD_Author, string the_Path_To_The_File, string cover,
int? number_Of_Printed_Pages, int? date_Of_Writing, int? the_Year_Of_Publishing,
string isbn, string time_To_Read, string about_The_Book, string age_Rating,
int is_Favorite, int iD_User)
        {
            this.Name = name;
            this.ID_Author = iD_Author;
            this.The_Path_To_The_File = the_Path_To_The_File;
            this.Cover = cover;
            this.Number_Of_Printed_Pages = number_Of_Printed_Pages;
            this.Date_Of_Writing = date_Of_Writing;
            this.The_Year_Of_Publishing = the_Year_Of_Publishing;
            this.ISBN = isbn;
            this.Time_To_Read = time_To_Read;
            this.About_The_Book = about_The_Book;
            this.Age_Rating = age_Rating;
            this.Is_Favorite = is_Favorite;
            this.ID_User = iD_User;
        }
        //public Book CopyFromBookDPO(BookDPO b)
        //{
        //    AuthorViewModel vmAuthor = new AuthorViewModel();
        //    int authorId = 0;
        //    foreach (var a in vmAuthor.ListAuthor)
        //    {
        //        if (a.LName == b.ID_Author)
        //        {
        //            authorId = a.Id;
        //            break;
        //        }
        //    }
        //    if (authorId != 0)
        //    {
        //        this.Id = b.Id;
        //        this.Name = b.Name;
        //        this.ID_Author = authorId;
        //        this.The_Path_To_The_File = b.The_Path_To_The_File;
        //        this.Cover = b.Cover;
        //        this.Number_Of_Printed_Pages = b.Number_Of_Printed_Pages;
        //        this.Date_Of_Writing = b.Date_Of_Writing;
        //        this.The_Year_Of_Publishing = b.The_Year_Of_Publishing;
        //        this.ISBN = b.ISBN;
        //        this.Time_To_Read = b.Time_To_Read;
        //        this.About_The_Book = b.About_The_Book;
        //        this.Age_Rating = b.Age_Rating;
        //        this.Is_Favorite = b.Is_Favorite;
        //        this.ID_User = b.ID_User; //----!!!!!!!!

        //    }
        //    return this;
        //}
        public Book ShallowCopy()
        {
            return (Book)this.MemberwiseClone();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
