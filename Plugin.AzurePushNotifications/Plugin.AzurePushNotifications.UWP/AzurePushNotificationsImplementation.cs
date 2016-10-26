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
            ReceivedMessageEventArgs content;

            switch (args.NotificationType)
            {
                case PushNotificationType.Badge:
                    content = new ReceivedMessageEventArgs(args.BadgeNotification?.Content.GetXml() ?? string.Empty, args.BadgeNotification);
                    break;
                case PushNotificationType.Tile:
                    content = new ReceivedMessageEventArgs(args.TileNotification?.Content.GetXml() ?? string.Empty, args.TileNotification);
                    break;
                case PushNotificationType.Toast:
                    content = new ReceivedMessageEventArgs(args.ToastNotification?.Content.GetXml() ?? string.Empty, args.ToastNotification);
                    break;

                case PushNotificationType.Raw:
                    content = new ReceivedMessageEventArgs(args.RawNotification?.Content ?? string.Empty, args.RawNotification);
                    break;

                default:
                    content = new ReceivedMessageEventArgs(string.Empty);
                    break;
            }

            var message = OnMessageReceived;
            message?.Invoke(null, content);
        }
    }
}