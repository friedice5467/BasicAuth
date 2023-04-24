using BasicAuth.DAL.Repo.Abstract;
using BasicAuth.Models;
using BasicAuth.Service;
using BasicAuth.Service.Abstract;
using System.ComponentModel.DataAnnotations;

namespace BasicAuth.Pages
{
    public partial class SignInPage : ContentPage
    {
        private readonly IAppStateService _appStateService;
        private readonly IAuthService _authService;

        private User userModel = new User();

        public SignInPage(IAppStateService appStateService)
        {
            InitializeComponent();
            _appStateService = appStateService;
            _authService = _appStateService.GetAuthService();
            BindingContext = userModel;
            NavigationPage.SetHasNavigationBar(this, false);
            DeviceDisplay.Current.MainDisplayInfoChanged += _appStateService.LockPortaitOrientation;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var validationContext = new ValidationContext(userModel);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(userModel, validationContext, validationResults, true);

            if (!isValid)
            {
                // Show an error message indicating that the User object is invalid
                string errorMessage = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
                await DisplayAlert("Validation Error", errorMessage, "OK");

                return;
            }

            bool validUser = false;

            try
            {
                _appStateService.IsBusy = true;
                validUser = await _authService.AuthenticateUserAsync(userModel.PhoneNumber, userModel.PasswordHash);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _appStateService.IsBusy = false;
            }


            if (validUser)
                await Navigation.PushAsync(new HomePage(_appStateService));
            
            else
                await DisplayAlert("Authentication", "Authentication failed. Please check your phone number and password.", "OK");
            
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage(_appStateService));
        }
    }
}
