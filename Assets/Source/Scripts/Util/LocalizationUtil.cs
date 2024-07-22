using System.Collections.Generic;
using Lean.Localization;

namespace SlimeScience.Util
{
    public static class LocalizationUtil
    {
        public static IReadOnlyDictionary<string, string> Languages = new Dictionary<string, string>()
        {
            { "ru", "Russian" },
            { "en", "English" },
            { "tr", "Turkish" },
        };
    }
}