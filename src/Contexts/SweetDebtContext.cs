using Microsoft.EntityFrameworkCore;
using SweetDebt.Models;

namespace SweetDebt.Contexts
{
    public class SweetDebtContext : DbContext
    {
        public SweetDebtContext(DbContextOptions<SweetDebtContext> options) : base(options) { }
        public DbSet<MyTransaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
