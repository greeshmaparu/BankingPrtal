using BankingPortal.API.Models;

namespace BankingPortal.API.Services
{
    public class AccountService: IAccountService
    {
        private static List<Account> account = new()
        {
            new Account
            {
                Id = 1,
                AccountNumber = "ACC1001",
                Balance = 500,
            },
            new Account
            {
                Id= 2,
                AccountNumber = "ACC1002",
                Balance=1000,
            }
        };
        public List<Account> GetAccount()
        {
            return account;
        }
    }
}
