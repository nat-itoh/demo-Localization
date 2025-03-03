using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using Project.Domain.Common;

namespace Project.Localization {

    /// <summary>
    /// 
    /// </summary>
    public sealed class LocaleDictionary {

        // Immutable dictionary
        private readonly Dictionary<Language, Locale> _languageToLocale;
        private readonly Dictionary<Locale, Language> _localeToLanguage;

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Language> Languages => _languageToLocale.Keys;

        // not safe
        public Locale this[Language language] => _languageToLocale[language];
        public Language this[Locale locale] => _localeToLanguage[locale];


        /// ----------------------------------------------------------------------------
        // Public Method

        public LocaleDictionary(IEnumerable<Locale> locales) {
            _languageToLocale = new();
            _localeToLanguage = new();

            foreach (Language language in Enum.GetValues(typeof(Language))) {
                string key = language.ToLocaleKey();
                var locale = locales.FirstOrDefault(l => l.Identifier.Code == key);
                if (locale == null)
                    continue;

                _languageToLocale[language] = locale;
                _localeToLanguage[locale] = language;
            }
        }

        public bool Contains(Language language) => _languageToLocale.ContainsKey(language);
        public bool Contains(Locale locale) => _localeToLanguage.ContainsKey(locale);

        public bool TryGetLocale(Language language, out Locale locale) => _languageToLocale.TryGetValue(language, out locale);
        public bool TryGetLanguage(Locale locale, out Language language) => _localeToLanguage.TryGetValue(locale, out language);
    }

}