using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.AzurePushNotifications
{
    /// <summary>
    /// Contains credentials for accessing Microsoft Azure Push Notifications 
    /// and Google Cloud Messaging.
    /// </summary>
    public class PushNotificationCredentials
    {
        /// <summary>
        /// Google API Project Number.
        /// </summary>
        public static string GoogleApiSenderId = string.Empty; 

        /// <summary>
        /// The name of the Azure Notification Hub created inside Azure portal.
        /// </summary>
        public static string AzureNotificationHubName = string.Empty;

        /// <summary>
        /// The listen connection string found under "Connection Information" 
        /// of the Notification Hub.
        /// </summary>
        public static string AzureListenConnectionString = string.Empty;

        
        /// <summary>
        /// The tags to register for with Azure Push Notifications.
        /// </summary>
        public static string[] Tags = new string[0];
    }
}
