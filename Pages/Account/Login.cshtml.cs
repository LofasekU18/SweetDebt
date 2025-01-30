using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SweetDebt.Service;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Principal;
using SweetDebt.Models;
using SQLitePCL;

namespace SweetDebt.Pages.Account;

[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly LoginService _loginService;
    [BindProperty]  
    public User LogginUser { get; set; }

    public LoginModel(LoginService loginService)
    {
        _loginService = loginService;
    }

    public void OnGet()
    {
      
    }
    public async Task<IActionResult> OnPostCreateTestUserAsync()
    {
        var IsTestAccount = await _loginService.VerifyUserAsync("test", "test");
        if (!IsTestAccount)
        {
            await _loginService.RegisterUserAsync("test", "test");
            return Redirect("~/");
        }
        ModelState.AddModelError(string.Empty, "Test account is already created.");
        return Redirect("~/");
    }
    public async Task<IActionResult> OnPost()
    {
        var user = LogginUser;
        if (ModelState.IsValid)
        {
            var verificationResult = await _loginService.VerifyUserAsync(user.Username,user.PasswordHash); 

            if (verificationResult)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, LogginUser.Username)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return Redirect("~/");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return Page();
    }
}
