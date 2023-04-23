namespace BasicAuth.Service.Abstract
{
    public interface IAuthService
    {
        Task InitializeAsync();
        bool IsLoggedIn { get; }
        event EventHandler AuthenticationStateChanged;
        void Logout();
        Task<bool> AuthenticateUserAsync(string phoneNumber, string password);
    }
}