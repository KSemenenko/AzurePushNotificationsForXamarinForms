using System.Text;
using Android.App;
using Android.Content;
using Gcm.Client;

namespace Plugin.AzurePushNotifications
{
    [Service]
    public class PushNotificationsGcmService : GcmServiceBase
    {
        protected override void OnMessage(Context context, Intent intent)
        {
            var msg = new StringBuilder();

            if(intent != null && intent.Extras != null)
            {
                foreach(var key in intent.Extras.KeySet())
                {
                    msg.AppendLine(key + "=" + intent.Extras.Get(key));
                }
            }

            var messageText = intent.Extras.GetString("message");
            if(!string.IsNullOrEmpty(messageText))
            {
                //createNotification("New hub message!", messageText);
                CrossAzurePushNotifications.Platform.PushNotificationReceived(messageText, messageText);
            }
            else
            {
                //createNotification("Unknown message details", msg.ToString());
                CrossAzurePushNotifications.Platform.PushNotificationReceived(msg.ToString(), intent.Extras);
            }
        }
    }
}