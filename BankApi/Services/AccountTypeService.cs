using BankApi.Data;
using Microsoft.EntityFrameworkCore;
using BankApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Services
{
    public class AccountTypeService
    {
        private readonly BankContext _context;

        public AccountTypeService(BankContext context)
        {
            _context = context;
        }   

       public async Task<AccountType?> GetAccountTypeByIdAsync(int id)
        {
            return await _context.AccountTypes.FindAsync(id);
        }



    }
}
