using System;
using System.Linq;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using Project.Domain.Common;
using UniRx;
using System.Threading;

// [REF]
//  LIGHT11: Localization スクリプトで操作する方法総まとめ https://light11.hatenadiary.com/entry/2022/03/10/200323#Locale%E3%82%92%E5%A4%89%E6%9B%B4%E3%81%99%E3%82%8B

namespace Project.Localization {

    public sealed class LocalizationService : IDisposable {
        
        private LocaleDictionary _localeDictionary;
        private readonly CancellationTokenSource _cts = new();


        public Language Selected => _localeDictionary[LocalizationSettings.SelectedLocale];

        public IObservable<Language> OnSelectedChanged { get; private set; }


        /// ----------------------------------------------------------------------------
        // Public Method

        public LocalizationService() {

        }

        public void Dispose() {
            _cts.Cancel();
            _cts.Dispose();
        }

        /// <summary>
        /// 初期化処理．
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public async UniTask InitializeAsync(Language language = Language.Japanese) {
            await LocalizationSettings.InitializationOperation.Task;

            // Contruct dictionay
            var availableLocales = LocalizationSettings.AvailableLocales.Locales;
            _localeDictionary = new(availableLocales);

            // Event stream
            OnSelectedChanged = ObservableLocalizationEvent
                .SelectedLocaleChangedAsObservable()
                .Select(locale => _localeDictionary[locale]);

            // First launguage
            LocalizationSettings.SelectedLocale = _localeDictionary[language];
        }

        /// <summary>
        /// 言語を変更する．
        /// </summary>
        /// <param name="launguage"></param>
        /// <returns></returns>
        public async UniTask ChangeLanguage(Language launguage, CancellationToken token = default) {
            token.ThrowIfCancellationRequested();

            // [NOTE] Only transition to registered languages
            if (!_localeDictionary.TryGetLocale(launguage, out var locale))
                return;

            if (LocalizationSettings.SelectedLocale == locale)
                return;

            // Chacnge
            LocalizationSettings.SelectedLocale = locale;
            {
                // Some process

            }

            // Wait initialization
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, token);
            await LocalizationSettings.InitializationOperation.WithCancellation(linkedCts.Token);
        }
    }
}
