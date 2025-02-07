using Microsoft.EntityFrameworkCore;
using SweetDebt.Contexts;
using SweetDebt.Models;

namespace SweetDebt.Service
{
    public class TransactionsService
    {
        private readonly SweetDebtContext _context;

        public TransactionsService(SweetDebtContext context)
        {
            _context = context;
        }
        public async Task<IList<MyTransaction>> GetTransactionsAsync()
        {
            if (_context != null)
            {
                return await _context.Transactions.ToListAsync();
            }
            return [];
        }
        public async Task AddTransactionAsync(MyTransaction transaction)
        {
            if (_context != null)
            {
                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();
            }
        }
        public decimal GetAmountType(MyTransaction transaction)
        {
            if (transaction.TypeOfTransaction == TypeOfTransaction.Positive)
                return transaction.Amount;
            else
                return -transaction.Amount;
        }
        public decimal GetTotalAmount(IList<MyTransaction> myTransactions)
        { return myTransactions.Sum((_transaction) => _transaction.TypeOfTransaction == TypeOfTransaction.Positive ? _transaction.Amount : -(_transaction.Amount));
        }
        public async Task DeleteAllTransactionsAsync()
        {
            if (_context != null)
            {
                await _context.Transactions.ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
            }
        }
    }
}
