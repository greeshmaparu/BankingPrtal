using BankingPortal.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService= accountService;
        }
        // GET: api/accounts
        [HttpGet]
        public IActionResult GetAccounts()
        {
            var account = _accountService.GetAccount();
            return Ok(account);
        }


        //// POST: api/accounts
        //[HttpPost]
        //public IActionResult AddAccount(string account)
        //{
        //    accounts.Add(account);

        //    return Ok(account);
        //}
    }
}