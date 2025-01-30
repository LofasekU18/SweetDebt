using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SweetDebt.Pages.Account;

[AllowAnonymous]
public class LoginModel : PageModel
{
    [TempData]
    public string ErrorMessage { get; set; }
    public string ReturnUrl { get; set; }
    [BindProperty, Required]
    public string Username { get; set; }
    [BindProperty, DataType(DataType.Password)]
    public string Password { get; set; }

    public void OnGet(string returnUrl = null)
    {
        //if (!string.IsNullOrEmpty(ErrorMessage))
        //{
        //    ModelState.AddModelError(string.Empty, ErrorMessage);
        //}

        //returnUrl = returnUrl ?? Url.Content("~/");

        //ReturnUrl = returnUrl;
    }
}
