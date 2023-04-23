using BasicAuth.Service.Abstract;
using System.Windows.Input;

namespace BasicAuth.Pages;

public partial class HomePage : ContentPage
{
    private readonly IAppStateService _appStateService;
    private readonly IAuthService _authService;

    public ICommand LogoutCommand { get; }

    public HomePage(IAppStateService appStateService)
    {
        InitializeComponent();
        _appStateService = appStateService;
        _authService = _appStateService.GetAuthService();
        NavigationPage.SetHasNavigationBar(this, false);
        LogoutCommand = new Command(Logout);
        BindingContext = this;
    }

    private async void Logout()
    {
        _appStateService.IsBusy = true;
        _authService.Logout();
        _appStateService.IsBusy = false;
        await Navigation.PushModalAsync(new NavigationPage(new SignInPage(_appStateService)));
    }
}
