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
        //public DbSet<User> Users => Set<User>();
        public DatabaseContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=SovaChte.db");
        }
    }
}