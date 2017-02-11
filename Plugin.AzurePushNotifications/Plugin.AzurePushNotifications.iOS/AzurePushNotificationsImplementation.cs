using System;
using System.Diagnostics;
using WindowsAzure.Messaging;
using Foundation;
using Plugin.AzurePushNotifications.Abstractions;
using UIKit;

namespace Plugin.AzurePushNotifications
{
    public partial class AzurePushNotificationsImplementation : IAzurePushNotifications
    {
        //https://azure.microsoft.com/en-us/documentation/articles/xamarin-notification-hubs-ios-push-notification-apns-get-started/
        private SBNotificationHub Hub { get; set; }

        public void RegisterForAzurePushNotification()
        {
            if(UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                    new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                var notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
            }
        }

        public void UnregisterFromAzurePushNotification()
        {
            UIApplication.SharedApplication.UnregisterForRemoteNotifications();
        }

        public void RegisteredForRemoteNotifications(NSData deviceToken)
        {
            Hub = new SBNotificationHub(PushNotificationCredentials.AzureListenConnectionString, PushNotificationCredentials.AzureNotificationHubName);

            Hub.UnregisterAllAsync(deviceToken, error =>
            {
                if(error != null)
                {
                    Console.WriteLine("Error calling Unregister: {0}", error.ToString());
                    return;
                }

                NSSet tags = new NSSet(PushNotificationCredentials.Tags);

                Hub.RegisterNativeAsync(deviceToken, tags, errorCallback =>
                {
                    if(errorCallback != null)
                    {
                        Console.WriteLine("RegisterNativeAsync error: " + errorCallback.ToString());
                    }
                });
            });

            // Register for Notifications
            UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(
                UIRemoteNotificationType.Alert |
                UIRemoteNotificationType.Badge |
                UIRemoteNotificationType.Sound);
        }

        public void ProcessNotification(NSDictionary options)
        {
            // Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
            if(null != options && options.ContainsKey(new NSString("aps")))
            {
                //Get the aps dictionary
                var aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

                var alert = string.Empty;

                //Extract the alert text
                // NOTE: If you're using the simple alert by just specifying
                // "  aps:{alert:"alert msg here"}  ", this will work fine.
                // But if you're using a complex alert with Localization keys, etc.,
                // your "alert" object from the aps dictionary will be another NSDictionary.
                // Basically the JSON gets dumped right into a NSDictionary,
                // so keep that in mind.
                if(aps.ContainsKey(new NSString("alert")))
                {
                    alert = (aps[new NSString("alert")] as NSString) ?? string.Empty;
                }

                //If this came from the ReceivedRemoteNotification while the app was running,
                // we of course need to manually process things like the sound, badge, and alert.

                //Manually show an alert
                if(!string.IsNullOrEmpty(alert))
                {
                    var avAlert = new UIAlertView("Notification", alert, null, "OK", null);
                    avAlert.Show();
                }

                var content = new ReceivedMessageEventArgs(alert,options);
                var message = OnMessageReceived;
                message?.Invoke(null, content);
            }
        }
    }
}