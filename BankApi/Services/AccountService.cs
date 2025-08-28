using BankApi.Models;
using BankApi.Data;
using Microsoft.EntityFrameworkCore;    


namespace BankApi.Services
{
    public class AccountService
    {
        private readonly BankContext _context;
        public AccountService(BankContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task UpdateAccountAsync(int id, Account account)
        {
            var existingAccount = await GetAccountByIdAsync(id);
            if (existingAccount is null)
            {
                throw new KeyNotFoundException("Account not found");
            }
            else
            {
                existingAccount.AccountType = account.AccountType;
                existingAccount.ClientID = account.ClientID;
                existingAccount.Balance = account.Balance;
                await _context.SaveChangesAsync();  
            }
        }

        public async Task DeleteAccountAsync(int id)
        {
            var accountToDelete = await GetAccountByIdAsync(id);
            if (accountToDelete is not null)
            {
                _context.Accounts.Remove(accountToDelete);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Account not found");
            }

        }
    }
}
