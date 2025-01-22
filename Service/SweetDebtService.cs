using Microsoft.EntityFrameworkCore;
using SweetDebt.Contexts;
using SweetDebt.Models;

namespace SweetDebt.Service
{
    public class SweetDebtService
    {
        private readonly SweetDebtContext _context;

        public SweetDebtService (SweetDebtContext context)
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
    }
}
