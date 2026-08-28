using BankingPortal.API.Data;
using BankingPortal.API.Models;
using BankingPortal.API.Services;
using Microsoft.EntityFrameworkCore;

namespace BankingPortal.API.Tests
{
    public class CustomerReportServiceTests
    {
        [Fact]
        public void GetHighRiskCustomers_ShouldReturnOnlyHighRiskCustomers()
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
                    Phone = "1111111111",
                    Risk = "Low"
                },
                new Customers
                {
                    Id = 2,
                    Name = "Mary",
                    Email = "mary@test.com",
                    Phone = "2222222222",
                    Risk = "High"
                },
                new Customers
                {
                    Id = 3,
                    Name = "David",
                    Email = "david@test.com",
                    Phone = "3333333333",
                    Risk = "High"
                }
            );

            context.SaveChanges();

            var service = new CustomerReportService(context);

            // Act
            var result = service.GetHighRiskCustomers();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, customer => Assert.Equal("High", customer.Risk));
        }
    }
}