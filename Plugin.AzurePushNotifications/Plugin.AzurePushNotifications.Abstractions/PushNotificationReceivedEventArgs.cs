using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.AzurePushNotifications.Abstractions
{
    public sealed class ReceivedMessageEventArgs
    {

        public ReceivedMessageEventArgs(string content)
        {
            Content = content;
        }
        public string Content { get;}
        
    }

    //

    public sealed class BadgeNotification 
    {
        //
        // Summary:
        //     Creates and initializes a new instance of the BadgeNotification.
        //
        // Parameters:
        //   content:
        //     The XML content that defines the badge update.
        public BadgeNotification(string content)
        {
            Content = content;
        }

        //
        // Summary:
        //     Gets the XML that defines the value or glyph used as the tile's badge.
        //
        // Returns:
        //     The object that contains the XML.
        public string Content { get; }
        //
        // Summary:
        //     Gets or sets the time that Windows will remove the badge from the tile.
        //
        // Returns:
        //     The date and time that the notification should be removed.
        public DateTimeOffset? ExpirationTime { get; set; }
    }

    public sealed class RawNotification 
    {
        //
        // Summary:
        //     Gets the content of the raw notification as a string. This string specifies a
        //     background task associated with the app.
        //
        // Returns:
        //     A string that contains the app-defined notification content, as set by the app
        //     server.
        public string Content { get; }
    }

    public enum PushNotificationType
    {
        //
        // Summary:
        //     A push notification to display as toast.
        Toast = 0,
        //
        // Summary:
        //     A push notification to update one or more elements of a tile.
        Tile = 1,
        //
        // Summary:
        //     A push notification to update a tile's badge overlay.
        Badge = 2,
        //
        // Summary:
        //     A push notification to perform an update to a tile that does not involve UI.
        Raw = 3
    }

    //
    // Summary:
    //     Defines an update to a tile, including its visuals, identification tag, and expiration
    //     time.

    public sealed class TileNotification 
    {
        //
        // Summary:
        //     Creates and initializes a new instance of the TileNotification object for use
        //     with a TileUpdater.
        //
        // Parameters:
        //   content:
        //     The object that provides the content for the tile notification.
        public TileNotification(string content)
        {
            Content = content;
        }

        //
        // Summary:
        //     Gets the XML description of the notification content, which you can then manipulate
        //     to alter the notification.
        //
        // Returns:
        //     The object that contains the notification content.
        public string Content { get; }
        //
        // Summary:
        //     Gets or sets the time that Windows will remove the notification from the tile.
        //
        // Returns:
        //     The date and time that the notification should be removed.
        public DateTimeOffset? ExpirationTime { get; set; }
        //
        // Summary:
        //     Gets or sets a string that Windows can use to prevent duplicate notification
        //     content from appearing in the queue.
        //
        // Returns:
        //     A string of 16 characters or less (plus a terminating null character) that identifies
        //     the notification in the stack. While there is no set form to the string content,
        //     we recommend that it should relate to the content of the notification.
        public string Tag { get; set; }
    }
}
