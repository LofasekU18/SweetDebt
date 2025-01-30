using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SweetDebt.Models;
using SweetDebt.Service;
using SweetDebt.Pages;

namespace SweetDebt.Pages;
public class IndexModel : PageModel
{
    public readonly TransactionsService _service;
    private bool Guest { get; set; } // Testing

    public IList<MyTransaction>? ListOfTransactions { get; set; }

    public decimal TotalAmount { get; set; }

    public bool AddTransactionVisible { get; set; }
    [BindProperty]
    public MyTransaction? NewTransaction { get; set; }

    public IndexModel(TransactionsService service)
    {
        _service = service;
    }

    public async Task OnGet()
    {
        ListOfTransactions = await _service.GetTransactionsAsync();
        TotalAmount = _service.GetTotalAmount(ListOfTransactions);
    }
    public async Task OnGetAddTransactionOpen()
    {
        ListOfTransactions = await _service.GetTransactionsAsync();
        TotalAmount = _service.GetTotalAmount(ListOfTransactions);
        AddTransactionVisible = true;
    }
    public IActionResult OnGetAddTransactionClose()
    {
        AddTransactionVisible = false;
        return RedirectToPage();
    }
    public async Task<IActionResult> OnPostAddTransactionSaveAsync()
    {

        if (!ModelState.IsValid)
        {
            AddTransactionVisible = true;
            ListOfTransactions = await _service.GetTransactionsAsync();
            TotalAmount = _service.GetTotalAmount(ListOfTransactions);
            return Page();
        }
        await _service.AddTransactionAsync(NewTransaction);
        return RedirectToPage();
    }
    public async Task<IActionResult> OnPostRemoveAllTransactionsAsync()
    {
        await _service.DeleteAllTransactionsAsync();
        return RedirectToPage();
    }
}