using System;
using UnityEngine.Localization.Settings;
using UniRx;

namespace UnityEngine.Localization {

    public static class ObservableLocalizationEvent {

        /// <summary>
        /// 
        /// </summary>
        public static IObservable<Locale> SelectedLocaleChangedAsObservable() {
            return Observable.FromEvent<Locale>(
                h => LocalizationSettings.SelectedLocaleChanged += h,
                h => LocalizationSettings.SelectedLocaleChanged -= h
            );
        }
    }
}
