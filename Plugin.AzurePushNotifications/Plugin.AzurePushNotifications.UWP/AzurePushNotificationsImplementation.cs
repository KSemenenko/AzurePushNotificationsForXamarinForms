using System;
using System.Diagnostics;
using Windows.Networking.PushNotifications;
using Microsoft.WindowsAzure.Messaging;
using Plugin.AzurePushNotifications.Abstractions;

namespace Plugin.AzurePushNotifications
{
    public partial class AzurePushNotificationsImplementation : IAzurePushNotifications
    {
        private PushNotificationChannel channel;

        public async void RegisterForAzurePushNotification()
        {
            if(channel != null)
            {
                channel.PushNotificationReceived -= Channel_PushNotificationReceived;
            }

            channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            var hub = new NotificationHub(PushNotificationCredentials.AzureNotificationHubName,
                PushNotificationCredentials.AzureListenConnectionString);

            await hub.RegisterNativeAsync(channel.Uri, PushNotificationCredentials.Tags);

            channel.PushNotificationReceived += Channel_PushNotificationReceived;
        }

        public async void UnregisterFromAzurePushNotification()
        {
            if(channel != null)
            {
                channel.PushNotificationReceived -= Channel_PushNotificationReceived;
            }

            var hub = new NotificationHub(PushNotificationCredentials.AzureNotificationHubName,
                PushNotificationCredentials.AzureListenConnectionString);

            await hub.UnregisterNativeAsync();

            channel = null;
        }

        private void Channel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            var conent = new ReceivedMessageEventArgs(args?.RawNotification?.Content ?? string.Empty);
            var message = OnMessageReceived;
            message?.Invoke(null, conent);
        }
    }
}