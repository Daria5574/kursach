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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-NSNU3CD4\SQLEXPRESS;Database=Sova;Trusted_Connection=true;TrustServerCertificate=true");
        }
        public DbSet<User> user { get; set; } = null!;
        public void AddUser(string? fName, string? lName, string email, string password)
        {
            var newUser = new User { FName = fName, LName = lName, Email = email, Password = password };
            user.Add(newUser);
            SaveChanges();
        }
        public DbSet<Author> author { get; set; } = null!;
        public void AddAuthor(string? fName, string lName, string? description, int? iD_User)
        {
            var newAuthor = new Author { FName = fName, LName = lName, Description = description, ID_User = iD_User };
            author.Add(newAuthor);
            SaveChanges();
        }
        public DbSet<Book> book { get; set; } = null!;
        public void AddBook(string name, int idAuthor, string pathToFile, string? cover, int? pages, int? writingDate, int? publishingYear, string? isbn, string? timeToRead, string? description, string? rating, int isFavorite, int iD_User)
        {
            var newBook = new Book { Name = name, ID_Author = idAuthor, The_Path_To_The_File = pathToFile, Cover = cover, Number_Of_Printed_Pages = pages, Date_Of_Writing = writingDate, The_Year_Of_Publishing = publishingYear, ISBN = isbn, Time_To_Read = timeToRead, About_The_Book = description, Age_Rating = rating, Is_Favorite = isFavorite, ID_User = iD_User };
            book.Add(newBook);
            SaveChanges();
        }
        public DbSet<Bookshelf> bookshelf { get; set; } = null!;

        public void AddBookshelf(int id_user, string name, string color_Code)
        {
            var newBookshelf = new Bookshelf { ID_User = id_user, Name = name, Color_Code = color_Code };
            bookshelf.Add(newBookshelf);
            SaveChanges();
        }

        public DbSet<Category> category { get; set; } = null!;
        public void AddCategory(string name)
        {
            var newCategory = new Category { Name = name };
            category.Add(newCategory);
            SaveChanges();
        }
        public DbSet<Genre> genre { get; set; } = null!;

        public void AddGenre(int iD_Category, string name)
        {
            var newGenre = new Genre { ID_Category = iD_Category, Name = name };
            genre.Add(newGenre);
            SaveChanges();
        }
        public DbSet<Theme> theme { get; set; } = null!;
        public void AddTheme(string name, int iD_User)
        {
            var newTheme = new Theme { Name = name, ID_User = iD_User };
            theme.Add(newTheme);
            SaveChanges();
        }

        public DbSet<Book_Bookshelf> book_bookshelf { get; set; } = null!;
        public void AddBook_Bookshelf(int iD_Book, int iD_Bookshelf)
        {
            var newBook_Bookshelf = new Book_Bookshelf { ID_Book = iD_Book, ID_Bookshelf = iD_Bookshelf };
            book_bookshelf.Add(newBook_Bookshelf);
            SaveChanges();
        }

        public DbSet<Book_Category> book_category { get; set; } = null!;

        public void AddBook_Category(int iD_Book, int iD_Category)
        {
            var newBook_Category = new Book_Category { ID_Book = iD_Book, ID_Category = iD_Category };
            book_category.Add(newBook_Category);
            SaveChanges();
        }

        public DbSet<Book_Theme> book_theme { get; set; } = null!;

        public void AddBook_Theme(int iD_Book, int iD_Theme)
        {
            var newBook_Theme = new Book_Theme { ID_Book = iD_Book, ID_Theme = iD_Theme };
            book_theme.Add(newBook_Theme);
            SaveChanges();
        }
    }
}