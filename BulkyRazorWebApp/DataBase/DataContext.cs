﻿using BulkyRazorWebApp.Model;
using Microsoft.EntityFrameworkCore;

namespace BulkyRazorWebApp.DataBase
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
            
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                  new Category { Id = 1, Name = "Sci-Fi", DisplayOrder = 1 },
                 new Category { Id = 2, Name = "Root", DisplayOrder = 2 },
                 new Category { Id = 3, Name = "High", DisplayOrder = 3 },
                 new Category { Id = 4, Name = "Major", DisplayOrder = 4 },
                 new Category { Id = 5, Name = "Hemon", DisplayOrder = 5 } );
        }
    }
}
