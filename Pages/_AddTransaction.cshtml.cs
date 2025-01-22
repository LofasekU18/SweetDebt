using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Transactions;
using SweetDebt.Models;
using SweetDebt.Service;

namespace SweetDebt.Pages
{
    public class _AddItemModalModel : PageModel
    {
        private readonly SweetDebtService _service;
        public IList<MyTransaction> ListOfTransactions { get; set; }
        
        public void OnGet()
        {
        }
        
    }
}
