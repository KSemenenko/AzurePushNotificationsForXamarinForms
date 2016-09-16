using System;
using System.Collections.Generic;
using System.Text;
using Plugin.AzurePushNotifications.Abstractions;

namespace Plugin.AzurePushNotifications
{
    public partial class CrossAzurePushNotifications
    {
        /// <summary>
        /// Current settings to use
        /// </summary>
        public static AzurePushNotificationsImplementation Platform
        {
            get
            {
                var ret = Implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return (AzurePushNotificationsImplementation)ret;
            }
        }
    }
}
