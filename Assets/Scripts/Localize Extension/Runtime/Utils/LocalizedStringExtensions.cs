using System;
using UnityEngine.Localization.Settings;
using UniRx;

namespace UnityEngine.Localization {

    public static class LocalizedStringExtensions {

        /// <summary>
        /// <see cref="LocalizedString.StringChanged"/>��Observable�Ƃ��Ĉ����g�����\�b�h�D
        /// </summary>
        public static IObservable<string> StringChangedAsObservable(this LocalizedString localizedString) {
            return Observable.Create<string>(observer => {
                void Handler(string newValue) => observer.OnNext(newValue);

                localizedString.StringChanged += Handler;

                return Disposable.Create(() => localizedString.StringChanged -= Handler);
            });
        }
    }
}
