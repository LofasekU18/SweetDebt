using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SweetDebt.Models;
using System.Collections.Generic;

public class IndexModel : PageModel
{
    public MyTransaction NewItem { get; set; }

    // Property to control modal visibility
    public bool AddTransactionVisible { get; set; }

    public void OnGet()
    {
        // Handle GET requests
    }

    // Open the AddTransaction
    public IActionResult OnGetAddTransactionOpen()
    {
        AddTransactionVisible = true;
        return Page();
    }

    // Close the AddTransaction
    public IActionResult OnGetAddTransactionClose()
    {
        AddTransactionVisible = false;
        return Page();
    }

    // Handle form submission
    public IActionResult OnPostAddTransactionSave()
    {
        if (!ModelState.IsValid)
        {
            AddTransactionVisible = true;
            return Page();
        }

        // Add the new item to the list
        //NewItem.Id = Items.Count + 1;
        //Items.Add(NewItem);


        AddTransactionVisible = false;
        return RedirectToPage();
    }
}
