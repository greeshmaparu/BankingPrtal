using BankingPortal.API.Models;

namespace BankingPortal.API.Services
{
    public interface IAccountService
    {
        List<Account>  GetAccount();
    }
}
