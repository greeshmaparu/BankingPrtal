using BankingPortal.API.Data;
using BankingPortal.API.Models;
using Microsoft.EntityFrameworkCore;
namespace BankingPortal.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankingDbContext _context;
        public CustomerService(BankingDbContext context)
        {
            _context = context;
        }        
        public List<Customers> GetCustomers()
        {
            return _context.Customers.ToList();
        }
        public Customers AddCustomer(Customers customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
           return customer;
        }
        public bool UpdateCustomer(Customers customer)
        {
            var existingCustomer = _context.Customers
                .FirstOrDefault(x => x.Id == customer.Id);

            if (existingCustomer == null)
            {
                return false;
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Risk = customer.Risk;

            _context.SaveChanges();

            return true;
        }
        public bool DeleteCustomer(int id)
        {
            var existingCustomer = _context.Customers
                .FirstOrDefault(x => x.Id == id);

            if (existingCustomer == null)
            {
                return false;
            }

            _context.Customers.Remove(existingCustomer);
            _context.SaveChanges();

            return true;
        }
    }
}
