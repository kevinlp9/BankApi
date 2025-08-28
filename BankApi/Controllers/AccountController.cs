using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankApi.Data;
using BankApi.Models;
using BankApi.Services;

namespace BankApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly AccountService _service;
        public AccountController(AccountService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Account>> GetAllAcounts()
        {
            return await _service.GetAllAccountsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccountByIdAsync(int id)
        {
            var account = await _service.GetAccountByIdAsync(id);
            if (account is null)
            {
                return AccountNotFounded(id);
            }

            return account;

        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountAsync(Account account)
        {
            var newAccount = await _service.CreateAccountAsync(account);
            return CreatedAtAction(nameof(GetAccountByIdAsync), new { id = newAccount.ID }, newAccount);
        }

        public NotFoundObjectResult AccountNotFounded(int id)
        {
            return NotFound(new { Message = $"Account with ID {id} not found." });
        }
    }
}
