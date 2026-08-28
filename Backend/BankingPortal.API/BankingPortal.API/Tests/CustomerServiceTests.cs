using BankingPortal.API.Models;
using BankingPortal.API.Services;
using Xunit;

namespace BankingPortal.API.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public void GetCustomers_ReturnsCustomerList()
        {
            // Arrange
            ICustomerService service = null!;

            // Act
            var result = service.GetCustomers();

            // Assert
            Assert.NotNull(result);
        }
    }
}