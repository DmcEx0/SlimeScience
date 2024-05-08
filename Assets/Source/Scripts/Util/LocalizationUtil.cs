using System.Collections.Generic;

namespace SlimeScience.Util
{
    public static class LocalizationUtil
    {
        public static IReadOnlyDictionary<string, string> Lanuages = new Dictionary<string, string>()
        {
            { "ru", "Russian" },
            { "en", "English" },
            { "tr", "Turkish" },
        };
    }
}