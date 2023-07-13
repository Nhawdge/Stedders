using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Stedders.Utilities
{
    internal static class TranslationManager
    {
        public static Dictionary<string, string> Translations { get; set; } = new();
        public static string GetTranslation(string key, string language = "en-US")
        {
            if (Translations.TryGetValue(key, out var value))
                return value;
            return $"{key} has no valid translations for {language}";
        }

        static TranslationManager()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                HeaderValidated = null,
            };
            using (var reader = new StreamReader("Assets/Translations.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<TranslationRow>();
                foreach (var row in records)
                {
                    Translations.Add(row.Key, row.Translation);
                }
            }
        }

        public record TranslationRow(string Key, string Translation);
    }
}
