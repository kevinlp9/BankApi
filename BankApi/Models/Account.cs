namespace BankApi.Models
{
    public class Account
    {
        public int ID { get; set; }
        public int AccountType { get; set; }
        public int? ClientID { get; set; }
        public decimal Balance { get; set; }
        public DateTime RegDate { get; set; } = DateTime.Now;

        //Relaciones

        public Client? Client { get; set; }
        public AccountType? AccountTypeNavigation { get; set; }
        public ICollection<BankTransaction> BankTransactions { get; set; } = new List<BankTransaction>();
    }

    public class AccountType
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime RegDate { get; set; } = DateTime.Now;
        // Relaciones
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }

    public class BankTransaction
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public int TransactionType { get; set; }
        public decimal Amount { get; set; }
        public int? ExternalAccount { get; set; }
        public DateTime RegDate { get; set; } = DateTime.Now;

        // Relaciones

        public Account? Account { get; set; }
        public TransactionType? TransactionTypeNavigation { get; set; }

    }

    public class Client
    {
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string? Email { get; set; }

        public DateTime RegDate { get; set; } = DateTime.Now;

        // Relaciones
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }


    public class TransactionType
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;

        public DateTime RegDate { get; set; } = DateTime.Now;
        // Relaciones
        public ICollection<BankTransaction> BankTransactions { get; set; } = new List<BankTransaction>();
    }
}
