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
        public async Task RegisterUserAsync(string username, string password, bool isAdmin=false)
        {
            var user = new User
            {
                Username = username,
                Password = _passwordHasher.HashPassword(null, password),
                IsAdmin = isAdmin
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
        public async Task<User> GetUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user != null && _passwordHasher.VerifyHashedPassword(null, user.Password, password) == PasswordVerificationResult.Success ? user : null;
        }
    }
}
