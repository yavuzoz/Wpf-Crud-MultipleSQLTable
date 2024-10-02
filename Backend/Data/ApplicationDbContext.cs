using System;
using Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> People { get; set; }
        public DbSet<Product> DeliveryLocations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQL Server bağlantı dizesi kullanarak veritabanını bağlama
            optionsBuilder.UseSqlServer("Server=DESKTOP-VQGH9P1\\SQLEXPRESS;Database=MyDatabase;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
