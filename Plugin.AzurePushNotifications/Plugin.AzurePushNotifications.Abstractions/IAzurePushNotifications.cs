using System;
using System.Globalization;

namespace Plugin.AzurePushNotifications.Abstractions
{
    /// <summary>
    ///     Interface for Plugin.AzurePushNotifications
    /// </summary>
    public interface IAzurePushNotifications
    {
        /// <summary>
        /// Registers for Azure Push Notification using 
        /// the credentials provided in PushNotificationCredentials class.
        /// </summary>
        void RegisterForAzurePushNotification();

        /// <summary>
        /// Unregisters from Azure Push Notification using 
        /// the credentials provided in PushNotificationCredentials class.
        /// </summary>
        void UnregisterFromAzurePushNotification();

        event EventHandler<ReceivedMessageEventArgs> OnMessageReceived;
    }
}