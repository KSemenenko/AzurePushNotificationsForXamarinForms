using System;
using System.Diagnostics;
using Android.App;
using Gcm.Client;
using Plugin.AzurePushNotifications.Abstractions;

namespace Plugin.AzurePushNotifications
{
    public partial class AzurePushNotificationsImplementation : IAzurePushNotifications
    {
        //https://azure.microsoft.com/en-us/documentation/articles/xamarin-notification-hubs-push-notifications-android-gcm/

        private Activity mainactivity;

        public void RegisterForAzurePushNotification()
        {
            if(mainactivity != null)
            {
                GcmClient.Register(mainactivity, PushNotificationCredentials.GoogleApiSenderId);
            }
        }

        public void UnregisterFromAzurePushNotification()
        {
            if(mainactivity != null)
            {
                GcmClient.UnRegister(mainactivity);
            }
        }

        public void InitFromMainActivity(Activity activity)
        {
            try
            {
                GcmClient.CheckDevice(activity);
                GcmClient.CheckManifest(activity);
                mainactivity = activity;
            }
            catch(Exception ex)
            {
                Logger.Debug(ex.Message);
            }
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