
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Transactions;

namespace Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // dbset
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
           

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().
                HasOne<Category>(i => i.Category)
                .WithMany(i => i.Products)
                .HasForeignKey(i => i.CategoryId);
        }
    }
}
