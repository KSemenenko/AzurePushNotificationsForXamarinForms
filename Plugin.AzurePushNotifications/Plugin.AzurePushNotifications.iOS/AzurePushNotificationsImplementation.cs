using System;
using System.IO;
using WindowsAzure.Messaging;
using AzurePushNotifications.Shared;
using Foundation;
using Plugin.AzurePushNotifications.Abstractions;

namespace Plugin.AzurePushNotifications
{
    public partial class AzurePushNotificationsImplementation : IAzurePushNotifications
    {
        public void RegisterForAzurePushNotification()
        {
            //// Connection string from your azure dashboard
            //var cs = SBConnectionString.CreateListenAccess(new NSUrl(PushNotificationCredentials.AzureNotificationHubName), PushNotificationCredentials.AzureListenConnectionString);

            //// Register our info with Azure
            //var hub = new SBNotificationHub(cs, "your-hub-name");
            //hub.RegisterNativeAsync(deviceToken, null, err => {
            //    if (err != null)
            //        Console.WriteLine("Error: " + err.Description);
            //    else
            //        Console.WriteLine("Success");
            //});
        }

        public void UnregisterFromAzurePushNotification()
        {
            throw new NotImplementedException();
        }
    }
}