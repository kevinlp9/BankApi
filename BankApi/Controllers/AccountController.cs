using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankApi.Data;
using BankApi.Models;
using BankApi.Services;

namespace BankApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService accountService;
        private readonly AccountTypeService accountTypeService; // Fixed type to AccountTypeService  
        private readonly ClientService clientService;

        public AccountController(AccountService accountService, AccountTypeService accountTypeService, ClientService clientService)
        {
            this.accountService = accountService;
            this.accountTypeService = accountTypeService; // Fixed type to AccountTypeService  
            this.clientService = clientService;
        }

        [HttpGet]
        public async Task<IEnumerable<Account>> GetAllAcounts()
        {
            return await accountService.GetAllAccountsAsync(); // Fixed incorrect service reference  
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccountByIdAsync(int id)
        {
            var account = await accountService.GetAccountByIdAsync(id); // Fixed incorrect service reference  
            if (account is null)
            {
                return AccountNotFounded(id);
            }

            return account;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountAsync(Account account)
        {
            var newAccount = await accountService.CreateAccountAsync(account); // Fixed incorrect service reference  
            return CreatedAtAction(nameof(GetAccountByIdAsync), new { id = newAccount.ID }, newAccount);
        }

        [NonAction]
        public async Task<string> ValidateAccount(Account account)
        {
            string result = "Valid";
            var accountType = await accountTypeService.GetAccountTypeByIdAsync(account.AccountType); // Fixed incorrect service reference  

            if (accountType is null)
                result = $"Account type with ID {account.AccountType} does not exist.";

            var clientID = account.ClientID.GetValueOrDefault();
            var client = await clientService.GetClientByIdAsync(clientID);
            if (client is null)
                result = $"Client with ID {account.ClientID} does not exist.";

            return result;
        }

        [NonAction]
        public NotFoundObjectResult AccountNotFounded(int id)
        {
            return NotFound(new { Message = $"Account with ID {id} not found." });
        }
    }
}
