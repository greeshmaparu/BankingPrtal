using BankingPortal.API.Data;
using BankingPortal.API.Models;

namespace BankingPortal.API.Services
{
    public class CustomerReportService
    {
        private readonly BankingDbContext _context;

        public CustomerReportService(BankingDbContext context)
        {
            _context = context;
        }

        public int GetCustomerCount()
        {
            return _context.Customers.Count();
        }
        public List<Customers> GetHighRiskCustomers()
        {
            return _context.Customers
                .Where(c => c.Risk == "High")
                .ToList();
        }
    }
}