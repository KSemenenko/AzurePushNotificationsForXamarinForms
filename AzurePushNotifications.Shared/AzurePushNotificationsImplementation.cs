using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using AzurePushNotifications.Shared.Parsers;
using Plugin.AzurePushNotifications.Abstractions;

namespace Plugin.AzurePushNotifications
{
    public partial class AzurePushNotificationsImplementation : IAzurePushNotifications
    {
        private Dictionary<string, Dictionary<string, string>> languageDictionary = new Dictionary<string, Dictionary<string, string>>();

        public AzurePushNotificationsImplementation()
        {
            CurrentCultureInfo = CultureInfo.CurrentCulture;
        }

        public AzurePushNotificationsImplementation(CultureInfo cultureInfo)
        {
            CurrentCultureInfo = cultureInfo;
        }

        public AzurePushNotificationsImplementation(string cultureInfo)
        {
            CurrentCulture = cultureInfo;
        }

        public CultureInfo CurrentCultureInfo { get; set; }

        public string Delimiter { get; set; } = string.Empty;

        public void LoadLanguagesFromFile(string path)
        {
            var content = FileLoad(path);

            CsvFileReader reader = string.IsNullOrEmpty(Delimiter) ? new CsvFileReader(content) : new CsvFileReader(content, Delimiter[0]);

            MakeDictionary(reader);
            FillDictionary(reader);

        }

        public void LoadLanguagesFromString(string content)
        {
            CsvFileReader reader = string.IsNullOrEmpty(Delimiter) ? new CsvFileReader(content) : new CsvFileReader(content, Delimiter[0]);

            MakeDictionary(reader);
            FillDictionary(reader);

        }

        private void MakeDictionary(CsvFileReader reader)
        {
            languageDictionary = new Dictionary<string, Dictionary<string, string>>();
            foreach (var header in reader.ReadHeader())
            {
                if(!string.IsNullOrEmpty(header))
                {
                    languageDictionary.Add(header.ToLowerInvariant(), new Dictionary<string, string>());
                }
            }
        }

        private void FillDictionary(CsvFileReader reader)
        {
            foreach (var row in reader.ReadRows())
            {
                int count = 1;
                foreach (var item in languageDictionary)
                {
                    item.Value.Add(row[0],row[count]);
                    count++;
                }
            }
        }


        public string this[string key]
        {
            get
            {
                Dictionary<string, string> langDictionary;
                if(!languageDictionary.TryGetValue(CurrentCulture, out langDictionary))
                {
                    langDictionary = languageDictionary.FirstOrDefault().Value;

                    if(langDictionary == null)
                    {
                        langDictionary = new Dictionary<string, string>();
                    }

                }

                string message;
                if (langDictionary.TryGetValue(key, out message))
                {
                    return message;
                }


                return string.Empty;
                
            }
            set
            {
                
            }
        }

        public string CurrentCulture
        {
            get { return CurrentCultureInfo.Name.ToLowerInvariant(); }
            set
            {
                CurrentCultureInfo = new CultureInfo(value);
            }
        }

        

    }
}