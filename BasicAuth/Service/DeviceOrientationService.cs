namespace BasicAuth.Service
{
    public partial class DeviceOrientationService
    {
        private partial void SetDeviceOrientationImplementation(DisplayOrientation displayOrientation);

        public void SetDeviceOrientation(DisplayOrientation displayOrientation)
        {
            SetDeviceOrientationImplementation(displayOrientation);
        }
    }
}
