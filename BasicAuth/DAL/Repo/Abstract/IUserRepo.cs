using BasicAuth.Models;

namespace BasicAuth.DAL.Repo.Abstract
{
    public interface IUserRepo
    {
        Task<bool> RegisterAsync(User user);
        Task<User> ValidateUserAsync(string phoneNumber, string password);
    }
}