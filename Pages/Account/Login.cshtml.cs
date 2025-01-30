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

namespace SweetDebt.Pages.Account;

[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly LoginService _loginService;
      
    public User LogginUser { get; set; }

    public LoginModel(LoginService loginService)
    {
        _loginService = loginService;
    }

    public void OnGet(string returnUrl = null)
    {
        //if (!string.IsNullOrEmpty(ErrorMessage))
        //{
        //    ModelState.AddModelError(string.Empty, ErrorMessage);
        //}

        //returnUrl = returnUrl ?? Url.Content("~/");

        //ReturnUrl = returnUrl;
    }
    public async Task<IActionResult> OnPost(string returnUrl)
    {
        returnUrl = returnUrl ?? Url.Content("~/");
        if (ModelState.IsValid)
        {
            var verificationResult = true; 

            if (verificationResult)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, LogginUser.Username)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return Redirect(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return Page();
    }
}
