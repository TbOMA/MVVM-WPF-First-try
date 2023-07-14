using Microsoft.EntityFrameworkCore;
using MVVM_FirsTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public ApplicationContext()
        {
            Database.Migrate();
        }
        public ApplicationContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(a => a.User)
                .WithMany(c => c.ClientOrders)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Order>()
                .HasOne(a => a.Car)
                .WithOne(c => c.Order)
                .HasForeignKey<Car>(c => c.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Data Source=localhost;Database=MVVM_FirstTry;Trusted_Connection=True;TrustServerCertificate=True;");
        }

    }

}
