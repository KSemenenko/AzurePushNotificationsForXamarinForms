using Android;
using Android.App;
using Android.Content;
using Gcm.Client;

[assembly: UsesPermission(Manifest.Permission.ReceiveBootCompleted)]

namespace Plugin.AzurePushNotifications
{
    [BroadcastReceiver(Permission = Constants.PermissionGcmIntents)]
    [IntentFilter(new[] {Intent.ActionBootCompleted})] // Allow GCM on boot and when app is closed   
    [IntentFilter(new[] {Constants.IntentFromGcmMessage}, Categories = new[] {"@PACKAGE_NAME@"})]
    [IntentFilter(new[] {Constants.IntentFromGcmRegistrationCallback}, Categories = new[] {"@PACKAGE_NAME@"})]
    [IntentFilter(new[] {Constants.IntentFromGcmLibraryRetry}, Categories = new[] {"@PACKAGE_NAME@"})]
    public class PushNotificationsBroadcastReceiver : GcmBroadcastReceiverBase<PushNotificationsGcmService>
    {
    }
}