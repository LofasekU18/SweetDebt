using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SweetDebt.Models;
using SweetDebt.Service;
using SweetDebt.Pages;

namespace SweetDebt.Pages;
public class IndexModel : PageModel
{
    public readonly SweetDebtService _service;
    
    public IList<MyTransaction>? ListOfTransactions { get; set; }

    public decimal TotalAmount { get; set; }
   
    public bool AddTransactionVisible { get; set; }
    [BindProperty]
    public MyTransaction? NewTransaction {  get; set; }

    public IndexModel (SweetDebtService service)
    {
        _service = service;
    }

    public async Task OnGet()
    {
        ListOfTransactions = await _service.GetTransactionsAsync();
        TotalAmount = _service.GetTotalAmount(ListOfTransactions);
    }

    // Open the AddTransaction
    public async Task OnGetAddTransactionOpen()
    {
        ListOfTransactions = await _service.GetTransactionsAsync();
        TotalAmount = _service.GetTotalAmount(ListOfTransactions);
        AddTransactionVisible = true;
    }
    public IActionResult OnGetAddTransactionClose()
    {
        //AddTransactionVisible = false;
        return RedirectToPage();
    }

    // Handle form submission
    public async Task<IActionResult> OnPostAddTransactionSave()
    {
        if (!ModelState.IsValid)
        {
            AddTransactionVisible = true;
            return Page();
        }

        await _service.AddTransactionAsync(NewTransaction);



        //AddTransactionVisible = false;
        return RedirectToPage();
    }
   
    

}
