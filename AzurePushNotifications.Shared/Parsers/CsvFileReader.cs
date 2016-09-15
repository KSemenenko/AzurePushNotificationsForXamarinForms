using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AzurePushNotifications.Shared.Parsers
{
    /// <summary>
    ///     Class to read data from a CSV file
    /// </summary>
    public class CsvFileReader : StreamReader
    {
        private readonly char delimiter;

        public CsvFileReader(Stream stream, char delimiter = ';')
            : base(stream)
        {
            this.delimiter = delimiter;
        }

        public CsvFileReader(string content, char delimiter = ';')
            : base(GenerateStreamFromString(content))
        {
            this.delimiter = delimiter;
        }

        public IEnumerable<CsvRow> ReadAllRows()
        {
            foreach(var line in ReadCompleteLine())
            {
                var keys = new Dictionary<string, string>();
                var guillemet = "_" + Guid.NewGuid() + "_";
                keys.Add(guillemet, "\"");

                var temp = line.Replace("\"\"", guillemet);

                var regex = new Regex(
                    @"""[^""]*""",
                    RegexOptions.IgnoreCase
                    | RegexOptions.Multiline
                    | RegexOptions.IgnorePatternWhitespace
                    );

                foreach(Match m in regex.Matches(temp))
                {
                    var k = "_" + Guid.NewGuid() + "_";
                    keys.Add(k, m.Value);
                    temp = temp.Replace(m.Value, k);
                }

                var row = new CsvRow();
                var split = temp.Split(delimiter);

                foreach(var s in split)
                {
                    row.Add(ReplaceKeys(s.Trim(), keys));
                }

                yield return row;
            }
        }

        public CsvRow ReadHeader()
        {
            var enumerator = ReadAllRows().GetEnumerator();
            if(enumerator.MoveNext())
            {
                if(enumerator.Current != null)
                {
                    return enumerator.Current;
                }
            }

            return new CsvRow();
        }

        public IEnumerable<CsvRow> ReadRows()
        {
            return ReadAllRows().Skip(1); //Skip Header
        }

        private string ReplaceKeys(string s, Dictionary<string, string> keys)
        {
            s = keys.Keys.Reverse().Aggregate(s, (current, key) => current.Replace(key, keys[key]));
            return s.StartsWith("\"") && s.EndsWith("\"") ? s.Substring(1, s.Length - 2) : s;
        }

        private IEnumerable<string> ReadCompleteLine()
        {
            string line;
            var completeLine = string.Empty;

            BaseStream.Position = 0;
            DiscardBufferedData();

            while ((line = ReadLine()) != null)
            {
                completeLine += "\n" + line;
                if(completeLine.ToCharArray().Count(c => c == '"')%2 == 0)
                {
                    yield return completeLine.Trim('\n');
                    completeLine = string.Empty;
                }
            }
        }

        public static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }
    }
}