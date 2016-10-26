using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.AzurePushNotifications.Abstractions
{
    public sealed class ReceivedMessageEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        public ReceivedMessageEventArgs(string content)
        {
            Content = content;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        public ReceivedMessageEventArgs(string content, object rawContent)
        {
            Content = content;
            RawContent = rawContent;
        }

        public string Content { get; }

        public object RawContent { get; }
    }
}