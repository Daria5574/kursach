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
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Bookshelf> Bookshelfs => Set<Bookshelf>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Theme> Themes => Set<Theme>();
        public DbSet<User> Users => Set<User>();

        public DatabaseContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=SovaChte.db");
        }
    }
}