using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Models
{
    public class StoreContext : IdentityDbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Cola Drink", Description = "sweet Drink", CreatedAt = DateTime.Now },
                new Product { Id = 2, Name = "Beer", Description = "Holly Drink", CreatedAt = DateTime.Now },
                new Product { Id = 3, Name = "Wine", Description = "Red", CreatedAt = DateTime.Now }
                );

        }

        public DbSet<Product> Products { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<Like> Likes { get; set; }
    }
}
