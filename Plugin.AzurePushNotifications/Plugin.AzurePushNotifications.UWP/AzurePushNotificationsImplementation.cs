using System;
using System.Diagnostics;
using System.IO;
using Windows.Networking.PushNotifications;
using AzurePushNotifications.Shared;
using Microsoft.WindowsAzure.Messaging;
using Plugin.AzurePushNotifications.Abstractions;

namespace Plugin.AzurePushNotifications
{
    public partial class AzurePushNotificationsImplementation : IAzurePushNotifications
    {
        private PushNotificationChannel channel;

        public async void RegisterForAzurePushNotification()
        {
            if (channel != null)
            {
                channel.PushNotificationReceived -= Channel_PushNotificationReceived;
            }

            channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            var hub = new NotificationHub(PushNotificationCredentials.AzureNotificationHubName,
                PushNotificationCredentials.AzureListenConnectionString);

            await hub.RegisterNativeAsync(channel.Uri, PushNotificationCredentials.Tags);

            channel.PushNotificationReceived += Channel_PushNotificationReceived;
        }

        private void Channel_PushNotificationReceived(PushNotificationChannel sender, Windows.Networking.PushNotifications.PushNotificationReceivedEventArgs args)
        {
            Debug.WriteLine("Channel_ShellToastNotificationReceived");
            Debug.WriteLine(args.RawNotification);
        }

        public async void UnregisterFromAzurePushNotification()
        {
            if (channel != null)
            {
                channel.PushNotificationReceived -= Channel_PushNotificationReceived;
            }

            var hub = new NotificationHub(PushNotificationCredentials.AzureNotificationHubName,
                PushNotificationCredentials.AzureListenConnectionString);

            await hub.UnregisterNativeAsync();

            channel = null;
        }
    }
}