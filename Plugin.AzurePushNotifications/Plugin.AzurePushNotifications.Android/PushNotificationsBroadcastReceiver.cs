using Android;
using Android.App;
using Android.Content;
using Gcm.Client;

[assembly: UsesPermission(Manifest.Permission.ReceiveBootCompleted)]

namespace Plugin.AzurePushNotifications
{
    [BroadcastReceiver(Permission = Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new[] {Intent.ActionBootCompleted})] // Allow GCM on boot and when app is closed   
    [IntentFilter(new[] {Constants.INTENT_FROM_GCM_MESSAGE}, Categories = new[] {"@PACKAGE_NAME@"})]
    [IntentFilter(new[] {Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK}, Categories = new[] {"@PACKAGE_NAME@"})]
    [IntentFilter(new[] {Constants.INTENT_FROM_GCM_LIBRARY_RETRY}, Categories = new[] {"@PACKAGE_NAME@"})]
    public class PushNotificationsBroadcastReceiver : GcmBroadcastReceiverBase<PushNotificationsGcmService>
    {
        //IMPORTANT: Change this to your own Sender ID!
        //The SENDER_ID is your Google API Console App Project Number
        public static string[] SENDER_IDS = {PushNotificationCredentials.GoogleApiSenderId};
        public const string TAG = "PushNotificationsBroadcastReceiver-GCM";
    }
}