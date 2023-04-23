using BasicAuth.DAL.Repo.Abstract;
using BasicAuth.Service.Abstract;
using BasicAuth.Service;

namespace BasicAuth.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepo _userRepo;
        private string _authToken;

        public AuthService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public bool IsLoggedIn => !string.IsNullOrEmpty(_authToken);

        public event EventHandler AuthenticationStateChanged;

        public void Logout()
        {
            try
            {
                SecureStorage.Remove("AuthToken");
            }
            catch (Exception ex)
            {
                // Handle or log the exception
            }
            finally
            {
                _authToken = null;
                AuthenticationStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        public async Task<bool> AuthenticateUserAsync(string phoneNumber, string password)
        {
            var user = await _userRepo.ValidateUserAsync(phoneNumber, password);

            if (user != null)
            {
                _authToken = AuthTokenGenerator.GenerateAuthToken(); // Generate a new auth token
                await SaveAuthTokenAsync(_authToken);
                AuthenticationStateChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }
            else
            {
                _authToken = null;
                AuthenticationStateChanged?.Invoke(this, EventArgs.Empty);
                return false;
            }
        }

        private async Task SaveAuthTokenAsync(string authToken)
        {
            try
            {
                await SecureStorage.SetAsync("AuthToken", authToken);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
            }
        }

        private async Task<string> LoadAuthTokenAsync()
        {
            try
            {
                return await SecureStorage.GetAsync("AuthToken");
            }
            catch (Exception ex)
            {
                // Handle or log the exception
            }

            return null;
        }

        public async Task InitializeAsync()
        {
            _authToken = await LoadAuthTokenAsync();
            AuthenticationStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
