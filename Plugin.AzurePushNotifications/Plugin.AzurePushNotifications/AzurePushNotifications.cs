using System;
using Plugin.AzurePushNotifications;
using Plugin.AzurePushNotifications.Abstractions;


namespace Plugin.AzurePushNotifications
{
    /// <summary>
    /// Cross platform Plugin.AzurePushNotifications implemenations
    /// </summary>
    public partial class CrossAzurePushNotifications
    {
        private static readonly Lazy<IAzurePushNotifications> Implementation = 
            new Lazy<IAzurePushNotifications>(CreatePluginAzurePushNotifications, System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Current settings to use
        /// </summary>
        public static IAzurePushNotifications Current
        {
            get
            {
                var ret = Implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        private static IAzurePushNotifications CreatePluginAzurePushNotifications()
        {
            return new AzurePushNotificationsImplementation();
        }

        static Exception NotImplementedInReferenceAssembly()
        {
            return
                new NotImplementedException(
                    "This functionality is not implemented in the portable version of this assembly.  " +
                    "You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}

