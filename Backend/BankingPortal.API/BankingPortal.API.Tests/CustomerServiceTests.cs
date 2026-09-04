using BankingPortal.API.Data;
using BankingPortal.API.Models;
using BankingPortal.API.Services;
using Microsoft.EntityFrameworkCore;

namespace BankingPortal.API.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public void GetCustomers_ShouldReturnAllCustomers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BankingDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new BankingDbContext(options);

            context.Customers.AddRange(
                new Customers
                {
                    Id = 1,
                    Name = "John",
                    Email = "john@test.com",
                    Phone = "1234567890",
                    Risk = "Low",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Address = "123 Main Street"
                },
                new Customers
                {
                    Id = 2,
                    Name = "Mary",
                    Email = "mary@test.com",
                    Phone = "0987654321",
                    Risk = "Medium",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Address = "1234 Main Street"
                }
            );

            context.SaveChanges();

            var service = new CustomerService(context);

            // Act
            var result = service.GetCustomers();

            // Assert
            Assert.Equal(2, result.Count);
        }
        [Fact]
        public void AddCustomer_WithoutEmail_ShouldThrowException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BankingDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new BankingDbContext(options);

            var service = new CustomerService(context);

            var customer = new Customers
            {
                Name = "John",
                Email = "",
                Phone = "1234567890",
                Risk = "Low"
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                service.AddCustomer(customer));
        }
    }

}