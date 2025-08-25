using Microsoft.EntityFrameworkCore;
using BankApi.Models;

namespace BankApi.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options){  }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet <BankTransaction> BankTransactions { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Client)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.ClientID);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.AccountTypeNavigation)
                .WithMany(at => at.Accounts)
                .HasForeignKey(a => a.AccountType);

            modelBuilder.Entity<BankTransaction>()
                .HasOne(bt => bt.Account)
                .WithMany(a => a.BankTransactions)
                .HasForeignKey(bt => bt.AccountID);

            modelBuilder.Entity<BankTransaction>()
                .HasOne(bt => bt.TransactionTypeNavigation)
                .WithMany(tt => tt.BankTransactions)
                .HasForeignKey(bt => bt.TransactionType);

        }

    }
}
