using System;
using System.Globalization;

namespace LogParser.Core.LineParser
{
    public static class LineParserPredefinedRules
    {
        public static Func<string, bool> NotNullOrEmpty = s => !string.IsNullOrEmpty(s);
        public static Func<string, bool> MaxLength(int maxLength) => s => s.Length <= maxLength;
        public static Func<string, bool> MinLength(int maxLength) => s => s.Length >= maxLength;

        public static Func<string, bool> DateFormat(string format) => s =>
            DateTime.TryParseExact(s, format, new CultureInfo("en-US"), DateTimeStyles.AssumeUniversal, out _);
    }
}