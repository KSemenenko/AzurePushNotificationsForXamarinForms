using System;

namespace Plugin.AzurePushNotifications.Abstractions
{
    /// <summary>
    ///     Interface for Plugin.AzurePushNotifications
    /// </summary>
    public interface IAzurePushNotifications
    {
        /// <summary>
        ///     Registers for Azure Push Notification using
        ///     the credentials provided in PushNotificationCredentials class.
        /// </summary>
        void RegisterForAzurePushNotification();

        /// <summary>
        ///     Unregisters from Azure Push Notification using
        ///     the credentials provided in PushNotificationCredentials class.
        /// </summary>
        void UnregisterFromAzurePushNotification();

        /// <summary>
        ///     It's a new message 
        /// </summary>
        event EventHandler<ReceivedMessageEventArgs> OnMessageReceived;
    }
}