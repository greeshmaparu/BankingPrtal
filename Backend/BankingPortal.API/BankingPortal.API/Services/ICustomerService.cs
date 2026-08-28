using BankingPortal.API.Models;

namespace BankingPortal.API.Services
{
    public interface ICustomerService
    {
        List<Customers> GetCustomers();
        Customers AddCustomer(Customers customer);
        bool UpdateCustomer(Customers customer);
        bool DeleteCustomer(int id);
    }
}
