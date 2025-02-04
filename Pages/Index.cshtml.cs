using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SweetDebt.Models;
using SweetDebt.Service;
using SweetDebt.Pages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace SweetDebt.Pages;
public class IndexModel : PageModel
{
    public readonly TransactionsService _service;
    public IList<MyTransaction>? ListOfTransactions { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsAdmin => User.HasClaim(c => c.Type == "IsAdmin" && c.Value == "True");
    public bool AddTransactionVisible { get; set; }
    [BindProperty]
    public MyTransaction NewTransaction { get; set; }

    public IndexModel(TransactionsService service)
    {
        _service = service;
    }
    public async Task<IActionResult> OnPostLogout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToPage("/Account/Login");
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
        if (ModelState.IsValid && IsAdmin)
        {
            await _service.AddTransactionAsync(NewTransaction);
            return RedirectToPage();
        }
        
        AddTransactionVisible = true;
        ListOfTransactions = await _service.GetTransactionsAsync();
        TotalAmount = _service.GetTotalAmount(ListOfTransactions);
        ModelState.AddModelError(string.Empty, "Not login as admin");
        return Page();
    }
    
    public async Task<IActionResult> OnPostRemoveAllTransactionsAsync()
    {
        if (IsAdmin)
        {
            await _service.DeleteAllTransactionsAsync();
            return RedirectToPage();
        }
        ListOfTransactions = await _service.GetTransactionsAsync();
        TotalAmount = _service.GetTotalAmount(ListOfTransactions);
        ModelState.AddModelError(string.Empty, "Not login as admin");
        return Page();
    }
}