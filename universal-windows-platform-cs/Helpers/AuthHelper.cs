using System;

using Windows.Security.ExchangeActiveSyncProvisioning;

namespace PassportLogin.Utils
{
    public static class AuthHelper
    {
        public static Guid GetDeviceId()
        {
            //Get the Device ID to pass to the server
            EasClientDeviceInformation deviceInformation = new EasClientDeviceInformation();
            return deviceInformation.Id;
        }
    }
}
