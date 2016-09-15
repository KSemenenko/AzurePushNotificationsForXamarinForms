using System;
using System.Collections.Generic;

namespace AzurePushNotifications.Shared.Parsers
{
    /// <summary>
    /// Class to store one CSV row
    /// </summary>
    public class CsvRow : List<string>
    {
        public string LineText { get; set; }

    }
}
