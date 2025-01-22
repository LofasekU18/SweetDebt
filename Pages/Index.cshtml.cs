using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SweetDebt.Models;
using SweetDebt.Service;
using SweetDebt.Pages;
public class IndexModel : PageModel
{
    private readonly SweetDebtService _service;
    
    public IList<MyTransaction> ListOfTransactions { get; set; }
    // Property to control modal visibility
    public bool AddTransactionVisible { get; set; }
    [BindProperty]
    public MyTransaction NewTransaction {  get; set; }

    public IndexModel (SweetDebtService service)
    {
        _service = service;
    }

    public async Task OnGet()
    {
        ListOfTransactions = await _service.GetTransactionsAsync();
    }

    // Open the AddTransaction
    public IActionResult OnGetAddTransactionOpen()
    {
        AddTransactionVisible = true;
        return Page();
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
