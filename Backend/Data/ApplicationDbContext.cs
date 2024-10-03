using System;
using Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Data
{
    //Database context class
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } // Changed from People to Categories
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQL Server connection string
            optionsBuilder.UseSqlServer("Server=DESKTOP-VQGH9P1\\SQLEXPRESS;Database=MyDatabase;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
