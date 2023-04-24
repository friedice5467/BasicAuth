using UIKit;

namespace BasicAuth.Service
{
    public partial class DeviceOrientationService
    {
        private static readonly IReadOnlyDictionary<DisplayOrientation, UIInterfaceOrientation> _iosDisplayOrientationMap =
            new Dictionary<DisplayOrientation, UIInterfaceOrientation>
            {
                [DisplayOrientation.Landscape] = UIInterfaceOrientation.LandscapeLeft,
                [DisplayOrientation.Portrait] = UIInterfaceOrientation.Portrait,
            };

        private partial void SetDeviceOrientationImplementation(DisplayOrientation displayOrientation)
        {
            if (_iosDisplayOrientationMap.TryGetValue(displayOrientation, out var iosOrientation))
                UIApplication.SharedApplication.SetStatusBarOrientation(iosOrientation, true);
        }
    }
}
