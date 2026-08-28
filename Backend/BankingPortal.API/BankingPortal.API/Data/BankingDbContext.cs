using BankingPortal.API.Models;
using Microsoft.EntityFrameworkCore;
namespace BankingPortal.API.Data
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(
            DbContextOptions<BankingDbContext> options)
            : base(options)
        {
        }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "12345",
                    Role = "Admin"
                },
                new User
                {
                    Id = 2,
                    Username = "employee",
                    Password = "12345",
                    Role = "Employee"
                }
            );
        }
        protected BankingDbContext()
        {
        }
    }
}
