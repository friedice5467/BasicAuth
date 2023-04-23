using BasicAuth.DAL.Repo;
using BasicAuth.DAL.Repo.Abstract;
using BasicAuth.Models;
using BasicAuth.Pages;
using BasicAuth.Service.Abstract;
using System.ComponentModel.DataAnnotations;

namespace BasicAuth.Pages;

public partial class RegistrationPage : ContentPage
{
    private RegistrationModel registrationModel = new RegistrationModel();
    private readonly IUserRepo _userRepo;
    private readonly IAppStateService _appStateService;

    public RegistrationPage(IAppStateService appStateService)
    {
        InitializeComponent();
        BindingContext = registrationModel;
        NavigationPage.SetHasNavigationBar(this, false);
        _appStateService = appStateService;
        _userRepo = _appStateService.GetUserRepo();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        List<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(registrationModel, new ValidationContext(registrationModel), validationResults, true);

        if (validationResults.Count == 0)
        {
            // set IsBusy to true before starting the registration process
            _appStateService.IsBusy = true;

            var user = new User
            {
                Email = registrationModel.Email,
                PhoneNumber = registrationModel.PhoneNumber,
                PasswordHash = registrationModel.Password
            };

            try
            {
                await _userRepo.RegisterAsync(user);

                // Show a message to the user indicating that registration was successful
                await DisplayAlert("Registration", "Registration successful!", "OK");

                // Navigate to the login page
                await Navigation.PushAsync(new SignInPage(_appStateService));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Registration Error", ex.Message, "OK");
            }

            // set IsBusy back to false when the registration process completes
            _appStateService.IsBusy = false;
        }
        else
        {
            string errorMessages = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
            await DisplayAlert("Validation Error", errorMessages, "OK");
        }
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignInPage(_appStateService));
    }
}