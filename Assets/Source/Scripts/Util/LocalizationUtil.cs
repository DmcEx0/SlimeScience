using System.Collections.Generic;

namespace SlimeScience.Util
{
    public static class LocalizationUtil
    {
        public static readonly IReadOnlyDictionary<string, string> Languages = new Dictionary<string, string>()
        {
            { "ru", "Russian" },
            { "en", "English" },
            { "tr", "Turkish" },
        };
    }
}