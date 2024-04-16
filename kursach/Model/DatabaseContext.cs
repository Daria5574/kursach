using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach.Model
{

    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public void AddUser(string fName, string? lName, string email, string password)
        {
            var newUser = new User { FName = fName, LName = lName, Email = email, Password = password };
            Users.Add(newUser);
            SaveChanges();
        }
        public DbSet<Author> Authors => Set<Author>();
        public void AddAuthor(string fName, string? lName, string? description, int idUser)
        {
            var newAuthor = new Author { FName = fName, LName = lName, Description = description, ID_User = idUser };
            Authors.Add(newAuthor);
            SaveChanges();
        }
        public DbSet<Book> Books => Set<Book>();
        public void AddBook(string name, int idAuthor, string pathToFile, string? cover, int? pages, int? writingDate, int? publishingYear, string? isbn, string? timeToRead, string? description, string? rating, int isFavorite, int idUser)
        {
            var newBook = new Book { Name = name, ID_Author = idAuthor, The_Path_To_The_File = pathToFile, Cover = cover, Number_Of_Printed_Pages = pages, Date_Of_Writing = writingDate, The_Year_Of_Publishing = publishingYear, ISBN = isbn, Time_To_Read = timeToRead, About_The_Book = description, Age_Rating = rating, Is_Favorite = isFavorite, ID_User = idUser };
            Books.Add(newBook);
            SaveChanges();
        }
        public DbSet<Bookshelf> Bookshelfs => Set<Bookshelf>();

        public void AddBookshelf(int id_user, string name, string color_Code)
        {
            var newBookshelf = new Bookshelf { ID_User = id_user, Name = name, Color_Code = color_Code };
            Bookshelfs.Add(newBookshelf);
            SaveChanges();
        }

        public DbSet<Category> Categories => Set<Category>();
        public void AddCategory(string name)
        {
            var newCategory = new Category { Name = name };
            Categories.Add(newCategory);
            SaveChanges();
        }
        public DbSet<Genre> Genres => Set<Genre>();

        public void AddGenre(int iD_Category, string name)
        {
            var newGenre = new Genre { ID_Category = iD_Category, Name = name };
            Genres.Add(newGenre);
            SaveChanges();
        }
        public DbSet<Theme> Themes => Set<Theme>();
        public void AddTheme(string name)
        {
            var newTheme = new Theme { Name = name };
            Themes.Add(newTheme);
            SaveChanges();
        }



        public DatabaseContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=SovaChte.db");
        }
    }
}