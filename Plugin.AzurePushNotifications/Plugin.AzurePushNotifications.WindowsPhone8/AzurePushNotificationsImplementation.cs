using System.Diagnostics;
using System.Linq;
using Microsoft.Phone.Notification;
using Microsoft.WindowsAzure.Messaging;
using Plugin.AzurePushNotifications.Abstractions;

namespace Plugin.AzurePushNotifications
{
    public partial class AzurePushNotificationsImplementation : IAzurePushNotifications
    {
        public void RegisterForAzurePushNotification()
        {
            var channel = HttpNotificationChannel.Find("MyPushChannel");
            if(channel == null)
            {
                channel = new HttpNotificationChannel("MyPushChannel");
                channel.Open();
                channel.BindToShellToast();

                channel.ChannelUriUpdated += async (o, args) =>
                {
                    var hub = new NotificationHub(PushNotificationCredentials.AzureNotificationHubName, PushNotificationCredentials.AzureListenConnectionString);

                    await hub.RegisterNativeAsync(args.ChannelUri.ToString(), PushNotificationCredentials.Tags);
                };
            }
            channel.ShellToastNotificationReceived += Channel_ShellToastNotificationReceived;
        }

        public void UnregisterFromAzurePushNotification()
        {
            var channel = HttpNotificationChannel.Find("MyPushChannel");
            if(channel == null)
            {
                channel = new HttpNotificationChannel("MyPushChannel");
                channel.Open();
                channel.BindToShellToast();
            }

            channel.ChannelUriUpdated += async (o, args) =>
            {
                var hub = new NotificationHub(PushNotificationCredentials.AzureNotificationHubName, PushNotificationCredentials.AzureListenConnectionString);

                await hub.UnregisterNativeAsync();
                await hub.UnregisterAllAsync(args.ChannelUri.ToString());
            };

            channel.ShellToastNotificationReceived -= Channel_ShellToastNotificationReceived;
        }

        private void Channel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            var conent = new ReceivedMessageEventArgs(string.Join(";", e?.Collection?.Select(x => x.Key + "=" + x.Value).ToArray() ?? new [] {string.Empty}),e?.Collection);
            var message = OnMessageReceived;
            message?.Invoke(null, conent);
        }
    }
}