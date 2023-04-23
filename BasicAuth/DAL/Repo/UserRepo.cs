using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using BasicAuth.DAL.Repo.Abstract;
using BasicAuth.Models;

namespace BasicAuth.DAL.Repo
{

    public class UserRepo : IUserRepo
    {
        private readonly IdentityDbContext _context;
        public UserRepo(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterAsync(User user)
        {
            try
            {
                if (await _context.Registrations.AnyAsync(u => u.Email == user.Email || u.PhoneNumber == user.PhoneNumber))
                    return false;

                // Hash the password using BCrypt.Net
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

                _context.Registrations.Add(user);
                int result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return true; // if the user was added successfully, return true
                }
                else
                {
                    return false; // otherwise, return false
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<User> ValidateUserAsync(string phoneNumber, string password)
        {
            var user = await _context.Registrations.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

            if (user == null)
            {
                return null; // user not found
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null; // password does not match
            }

            return user; // user and password match
        }

}
}
