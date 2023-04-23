using BasicAuth.Pages;
using BasicAuth.Service.Abstract;

namespace BasicAuth;

public partial class App : Application
{
    private readonly IAppStateService _appStateService;
    private IAuthService _authService;

    public App(IAppStateService appStateService)
    {
        InitializeComponent();
        _appStateService = appStateService;
        _authService = _appStateService.GetAuthService();
        Task.Run(_authService.InitializeAsync);
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var page = _authService.IsLoggedIn ? (Page)new HomePage(_appStateService) : new SignInPage(_appStateService);
        var navigationPage = new NavigationPage(page);

        return new Window(navigationPage);
    }
}
