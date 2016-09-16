# AzurePushNotificationsForXamarinForms
Azure Push Notifications for Xamarin Forms


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
add in to MainActivity
```cs
protected override void OnCreate(Bundle bundle)
{
    base.OnCreate(bundle);

    global::Xamarin.Forms.Forms.Init(this, bundle);

    CrossAzurePushNotifications.Platform.InitFromMainActivity(this); // for init

    LoadApplication(new TestFormsApp.App());
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