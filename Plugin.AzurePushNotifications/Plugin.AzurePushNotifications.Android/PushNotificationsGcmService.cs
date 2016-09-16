using System;
using System.Collections.Generic;
using System.Text;
using WindowsAzure.Messaging;
using Android;
using Android.App;
using Android.Content;
using Android.Util;
using Gcm.Client;
using Java.Net;

namespace Plugin.AzurePushNotifications
{
    [Service] //Must use the service tag
    public class PushNotificationsGcmService : GcmServiceBase
    {
        private static NotificationHub hub;

        public PushNotificationsGcmService() : base(PushNotificationsBroadcastReceiver.SENDER_IDS)
        {
        }

        public static void Initialize(Context context)
        {
            hub = new NotificationHub(PushNotificationCredentials.AzureNotificationHubName, PushNotificationCredentials.AzureListenConnectionString, context);
        }

        public static void Register(Context Context)
        {
            // Makes this easier to call from our Activity
            GcmClient.Register(Context, PushNotificationsBroadcastReceiver.SENDER_IDS);
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            //Receive registration Id for sending GCM Push Notifications to

            if(hub != null)
            {
                hub.Register(registrationId, "TEST");
            }

            ////////////////

            Log.Verbose(PushNotificationsBroadcastReceiver.TAG, "GCM Registered: " + registrationId);

            createNotification("PushHandlerService-GCM Registered...",
                "The device has been Registered!");

            hub = new NotificationHub(PushNotificationCredentials.AzureNotificationHubName, PushNotificationCredentials.AzureListenConnectionString, context);
            try
            {
                hub.UnregisterAll(registrationId);
            }
            catch(Exception ex)
            {
                Log.Error(PushNotificationsBroadcastReceiver.TAG, ex.Message);
            }

            //var tags = new List<string>() { "falcons" }; // create tags if you want
            var tags = new List<string>();

            try
            {
                var hubRegistration = hub.Register(registrationId, tags.ToArray());
            }
            catch(Exception ex)
            {
                Log.Error(PushNotificationsBroadcastReceiver.TAG, ex.Message);
            }
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            if(hub != null)
            {
                hub.Unregister();
            }
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            Console.WriteLine("Received Notification");

            //Push Notification arrived - print out the keys/values
            if(intent != null || intent.Extras != null)
            {
                var keyset = intent.Extras.KeySet();

                foreach(var key in intent.Extras.KeySet())
                {
                    Console.WriteLine("Key: {0}, Value: {1}", key, intent.Extras.GetString(key));
                }
            }

            ////////////////////////

            Log.Info(PushNotificationsBroadcastReceiver.TAG, "GCM Message Received!");

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
                CrossAzurePushNotifications.Platform.PushNotificationReceived(messageText);
            }
            else
            {
                //createNotification("Unknown message details", msg.ToString());
                CrossAzurePushNotifications.Platform.PushNotificationReceived(msg.ToString());
            }
        }

        protected override bool OnRecoverableError(Context context, string errorId)
        {
            //Some recoverable error happened
            return true;
        }

        protected override void OnError(Context context, string errorId)
        {
            //Some more serious error happened
        }

        private void createNotification(string title, string desc)
        {
            //Create notification
            var notificationManager = GetSystemService(NotificationService) as NotificationManager;

            ////Create an intent to show UI
            //var uiIntent = new Intent(this, typeof(MainActivity));

            ////Create the notification
            //var notification = new Notification(Resource.Drawable.SymActionEmail, title);

            ////Auto-cancel will remove the notification once the user touches it
            //notification.Flags = NotificationFlags.AutoCancel;

            ////Set the notification info
            ////we use the pending intent, passing our ui intent over, which will get called
            ////when the notification is tapped.
            //notification.SetLatestEventInfo(this, title, desc, PendingIntent.GetActivity(this, 0, uiIntent, 0));

            ////Show the notification
            //notificationManager.Notify(1, notification);
            //dialogNotify(title, desc);
        }

        protected void dialogNotify(string title, string message)
        {
            //MainActivity.instance.RunOnUiThread(() =>
            //{
            //    var dlg = new AlertDialog.Builder(MainActivity.instance);
            //    var alert = dlg.Create();
            //    alert.SetTitle(title);
            //    alert.SetButton("Ok", delegate { alert.Dismiss(); });
            //    alert.SetMessage(message);
            //    alert.Show();
            //});
        }
    }
}