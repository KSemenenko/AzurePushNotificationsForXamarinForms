using System;
using System.Diagnostics;
using System.IO;
using Android.App;
using Android.Provider;
using Plugin.AzurePushNotifications.Abstractions;
using Android.Util;
using Gcm.Client;

namespace Plugin.AzurePushNotifications
{
    public partial class AzurePushNotificationsImplementation : IAzurePushNotifications
    {
        //https://azure.microsoft.com/en-us/documentation/articles/xamarin-notification-hubs-push-notifications-android-gcm/
        public void RegisterForAzurePushNotification()
        {
         
        }

        public void UnregisterFromAzurePushNotification()
        {
            throw new NotImplementedException();
        }

        public void InitFromMainActivity(Activity activity)
        {
            GcmClient.CheckDevice(activity);
            GcmClient.CheckManifest(activity);

            // Register for push notifications
            Log.Info("MainActivity", "Registering...");
            GcmClient.Register(activity, PushNotificationCredentials.GoogleApiSenderId);
        }

        public void PushNotificationReceived(string content)
        {
            var conent = new ReceivedMessageEventArgs(content);
            var message = OnMessageReceived;
            message?.Invoke(null, conent);
            Debug.WriteLine("Channel_PushNotificationReceived");
        }
    }
}