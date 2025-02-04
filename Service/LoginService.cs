using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SweetDebt.Contexts;
using SweetDebt.Models;

namespace SweetDebt.Service
{
    public class LoginService
    {
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        private SweetDebtContext _context;
        public LoginService(SweetDebtContext context)
        {
            _context = context;
        }
        public async Task RegisterUserAsync(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = _passwordHasher.HashPassword(null, password)
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> VerifyUserAsync(string username, string password)
        {

            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user == null) return false;
                
                var passwordVerification = _passwordHasher.VerifyHashedPassword(null, user.Password, password);


                return passwordVerification == PasswordVerificationResult.Success;
            }
        }
    }
}
