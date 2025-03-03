using System;
using System.Linq;
using System.Collections.Generic;

namespace Project.Domain.Common {

    // [NOTE]
    //  Except for things that are directly affected by localization, such as the UI scripts,
    //  the entire app uses the Language enumeration type, which does not depend on "Unity Localization Package".

    /// <summary>
    /// Languages used in the application.
    /// </summary>
    public enum Language {
        Japanese,
        English,
        Chinese,
        German,
        French,
        Dutch,
        Italian,
        Spanish,
        Czech,

        // 
        Other,
    }


    public static class LanguageUtils {

        public static string ToLocaleKey(this Language launguage) =>
            launguage switch {
                // [NOTE] Must match "Locale code"
                Language.Japanese => "ja",
                Language.English => "en",
                Language.Chinese => "zh-Hans",
                Language.German => "de",
                Language.French => "fr",
                Language.Dutch => "nl",
                Language.Italian => "it",
                Language.Spanish => "es",
                Language.Czech => "cs",
                // 
                Language.Other => "",
                _ => throw new System.NotImplementedException()
            };

        public static Language FromLocaleKey(string key) {
            if (string.IsNullOrEmpty(key))
                return Language.Other;

            foreach (Language lang in Enum.GetValues(typeof(Language))) {
                if (lang.ToLocaleKey() == key)
                    return lang;
            }
            return Language.Other;
        }
    }
}
