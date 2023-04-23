
using BasicAuth.Service.Abstract;

namespace BasicAuth.Pages
{
    public class BusyIndicator : ContentView
    {
        public BusyIndicator()
        {
            var loadingLabel = new Label
            {
                Text = "Loading...",
                FontSize = (double)new FontSizeConverter().ConvertFromInvariantString("Large"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            var loadingLayout = new Frame
            {
                Content = loadingLabel,
                BackgroundColor = new Color(0, 0, 0, 0.5f),
                Padding = new Thickness(20),
                HasShadow = false,
                WidthRequest = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density / 2,
                HeightRequest = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density / 2,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            var grid = new Grid
            {
                Children = { loadingLayout },
                BackgroundColor = new Color(0, 0, 0, 0.5f),
                IsVisible = false
            };

            Content = grid;
        }
    }
}
