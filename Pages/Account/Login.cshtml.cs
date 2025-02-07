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
    public User LogginUser { get; set; } // using PasswordHash like Password for check login

    public LoginModel(LoginService loginService)
    {
        _loginService = loginService;
    }
    public async Task OnGet()
    {
        //await _loginService.RegisterUserAsync("admin", "test", true);
    }
    public async Task<IActionResult> OnPostCreateTestUserAsync()
    {
        var IsTestAccount = await _loginService.VerifyUserAsync("test", "test");

        if (!IsTestAccount)
        {
            await _loginService.RegisterUserAsync("test", "test");
            return Page();

        }
        ModelState.AddModelError(string.Empty, "Test account is already exist.");
        return Page();

    }
    public async Task<IActionResult> OnPostLoginAsync()
    {
        var userLogin = LogginUser;
        if (ModelState.IsValid)
        {
            var user = await _loginService.GetUserAsync(userLogin.Username, userLogin.Password);

            if (user != null && user.IsAdmin == true)
            {
                LogginUser.IsAdmin = user.IsAdmin;
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, LogginUser.Username),
                    new Claim("IsAdmin", LogginUser.IsAdmin.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return Redirect("~/");
            }
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, LogginUser.Username)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = false, ExpiresUtc = null });

                return Redirect("~/");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return Page();
    }
}
