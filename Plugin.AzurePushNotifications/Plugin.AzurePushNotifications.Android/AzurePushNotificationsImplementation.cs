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

        public void RegisterForAzurePushNotification()
        {
            if(GcmClient.MainActivity != null)
            {
                GcmClient.Register(GcmClient.MainActivity, PushNotificationCredentials.GoogleApiSenderId);
            }
        }

        public void UnregisterFromAzurePushNotification()
        {
            if(GcmClient.MainActivity != null)
            {
                GcmClient.UnRegister(GcmClient.MainActivity);
            }
        }

        public void Init(Activity activity)
        {
            try
            {
                GcmClient.CheckDevice(activity);
                GcmClient.CheckManifest(activity);
                GcmClient.MainActivity = activity;
            }
            catch(Exception ex)
            {
                //Logger.Debug(ex.Message);
            }
        }

        public void PushNotificationReceived(string content, object rawContent)
        {
            var conent = new ReceivedMessageEventArgs(content, rawContent);
            var message = OnMessageReceived;
            message?.Invoke(null, conent);
        }
    }
}