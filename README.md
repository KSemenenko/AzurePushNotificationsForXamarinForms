# AzurePushNotificationsForXamarinForms
Azure Push Notifications for Xamarin Forms

# Alpha version

## Available at NuGet. 
https://www.nuget.org/packages/ksemenenko.AzurePushNotifications/

|Platform|Supported|Version|
| ------------------- | :-----------: | :------------------: |
|Xamarin.iOS Unified|Yes|iOS 6+|
|Xamarin.Android|Yes|API 10+|
|Windows Phone 8|Yes|8.0+|
|Windows Phone 8.1|Yes|8.1+|
|Windows Store|Yes|8.1+|
|Windows 10 UWP|Yes|10+|



How to use?


## iOS
add in to AppDelegate
```cs

public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
{
    CrossAzurePushNotifications.Platform.ProcessNotification(userInfo);
}

public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
{
    CrossAzurePushNotifications.Platform.RegisteredForRemoteNotifications(deviceToken);
}

```

## Android
Add in to MainActivity
```cs
protected override void OnCreate(Bundle bundle)
{
    base.OnCreate(bundle);

    global::Xamarin.Forms.Forms.Init(this, bundle);

    CrossAzurePushNotifications.Platform.InitFromMainActivity(this); // for init

    LoadApplication(new TestFormsApp.App());
}

```

### For yours custom BroadcastReceiver


#### Create Class like this:
```cs
[BroadcastReceiver(Permission = Constants.PermissionGcmIntents)]
[IntentFilter(new[] {Intent.ActionBootCompleted})] // Allow GCM on boot and when app is closed   
[IntentFilter(new[] {Constants.IntentFromGcmMessage}, Categories = new[] {"@PACKAGE_NAME@"})]
[IntentFilter(new[] {Constants.IntentFromGcmRegistrationCallback}, Categories = new[] {"@PACKAGE_NAME@"})]
[IntentFilter(new[] {Constants.IntentFromGcmLibraryRetry}, Categories = new[] {"@PACKAGE_NAME@"})]
public class PushNotificationsBroadcastReceiver : GcmBroadcastReceiverBase<YOUR_SERVICE_CLASS>
{
}
```

#### Create service class like this:
```cs
[Service] //Must use the service tag
public class YOUR_SERVICE_CLASS : GcmServiceBase
{
    protected override void OnMessage(Context context, Intent intent)
    {
        Console.WriteLine("Received Notification");   
    } 
}
```


## In to App.cs
```cs
PushNotificationCredentials.Tags = new string[] { };
PushNotificationCredentials.GoogleApiSenderId = "google sender id";
PushNotificationCredentials.AzureNotificationHubName = "hub name";
PushNotificationCredentials.AzureListenConnectionString = "Endpoint";

CrossAzurePushNotifications.Current.RegisterForAzurePushNotification();

CrossAzurePushNotifications.Current.OnMessageReceived += (sender, ev) =>
{
    Debug.WriteLine(ev.Content);
};
```


## Thanks
https://github.com/Redth/GCM.Client
https://azure.microsoft.com/en-us/documentation/articles/xamarin-notification-hubs-ios-push-notification-apns-get-started/
https://azure.microsoft.com/en-us/documentation/articles/xamarin-notification-hubs-push-notifications-android-gcm/
https://components.xamarin.com/gettingstarted/azure-messaging
https://github.com/HoussemDellai/Azure-Push-Notification-For-Xamarin