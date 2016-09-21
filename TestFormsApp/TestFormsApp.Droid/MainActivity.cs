using System;
using System.IO;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Gcm.Client;
using Plugin.AzurePushNotifications;
using Environment = System.Environment;
using WindowsAzure.Messaging;

namespace TestFormsApp.Droid
{
    [Activity(Label = "TestFormsApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            CrossAzurePushNotifications.Platform.Init(this);

            LoadApplication(new TestFormsApp.App());
        }
    }

    [BroadcastReceiver(Permission = Constants.PermissionGcmIntents)]
    [IntentFilter(new[] {Intent.ActionBootCompleted})] // Allow GCM on boot and when app is closed   
    [IntentFilter(new[] {Constants.IntentFromGcmMessage}, Categories = new[] {"@PACKAGE_NAME@"})]
    [IntentFilter(new[] {Constants.IntentFromGcmRegistrationCallback}, Categories = new[] {"@PACKAGE_NAME@"})]
    [IntentFilter(new[] {Constants.IntentFromGcmLibraryRetry}, Categories = new[] {"@PACKAGE_NAME@"})]
    public class PushNotificationsBroadcastReceiver : GcmBroadcastReceiverBase<MyMessageService>
    {
    }

    [Service] //Must use the service tag
    public class MyMessageService : GcmServiceBase
    {
        protected override void OnMessage(Context context, Intent intent)
        {
            Console.WriteLine("Received Notification");
        }
    }
}