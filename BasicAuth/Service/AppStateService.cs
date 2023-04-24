using System.ComponentModel;
using BasicAuth.DAL.Repo.Abstract;
using BasicAuth.Pages;
using BasicAuth.Service.Abstract;

namespace BasicAuth.Service
{
    public class AppStateService : IAppStateService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DeviceOrientationService _deviceOrientationService;
        public AppStateService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _deviceOrientationService = new DeviceOrientationService();
            ShowBusyIndicator();
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBusy)));
                if (IsBusy)
                {
                    ShowBusyIndicator();
                }
                else
                {
                    HideBusyIndicator();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IAuthService GetAuthService()
        {
            return (IAuthService)_serviceProvider.GetService(typeof(IAuthService));
        }

        public IUserRepo GetUserRepo()
        {
            return (IUserRepo)_serviceProvider.GetService(typeof(IUserRepo));
        }

        private void ShowBusyIndicator()
        {
            if (Application.Current?.MainPage is NavigationPage navPage)
            {
                // Get the current page being displayed
                if (navPage.CurrentPage is ContentPage page)
                {
                    ShowBusyIndicatorForPage(page);
                }
            }
            else if (Application.Current?.MainPage is ContentPage contentPage)
            {
                ShowBusyIndicatorForPage(contentPage);
            }
        }

        private void ShowBusyIndicatorForPage(ContentPage page)
        {
            if (page.Content is Grid grid)
            {
                if (grid.Children.Count > 1 && grid.Children[1] is BusyIndicator indicator)
                {
                    indicator.IsVisible = true;
                    indicator.IsEnabled = true;
                    indicator.Content.IsVisible = true;
                    indicator.Content.IsEnabled = true;
                }
            }
        }
        private void HideBusyIndicator()
        {
            if (Application.Current?.MainPage is NavigationPage navPage)
            {
                // Get the current page being displayed
                if (navPage.CurrentPage is ContentPage page)
                {
                    HideBusyIndicatorForPage(page);
                }
            }
            if (Application.Current?.MainPage is ContentPage contentPage)
            {
                HideBusyIndicatorForPage(contentPage);
            }
        }

        private void HideBusyIndicatorForPage(ContentPage page)
        {
            if (page.Content is Grid grid)
            {
                if (grid.Children.Count > 1 && grid.Children[1] is BusyIndicator indicator)
                {
                    indicator.IsVisible = false;
                    indicator.IsEnabled = false;
                    indicator.Content.IsVisible = false;
                    indicator.Content.IsEnabled = false;
                }
            }
        }

        public void LockPortaitOrientation(object sender, DisplayInfoChangedEventArgs e)
        {
            switch (DeviceDisplay.Current.MainDisplayInfo.Orientation)
            {
                case DisplayOrientation.Landscape:
                    _deviceOrientationService.SetDeviceOrientation(DisplayOrientation.Portrait);
                    break;
                case DisplayOrientation.Portrait:
                    _deviceOrientationService.SetDeviceOrientation(DisplayOrientation.Portrait);
                    break;
            }
        }

    }
}
