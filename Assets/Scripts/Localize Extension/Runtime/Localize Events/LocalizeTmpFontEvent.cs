using System;
using UnityEngine.Events;
using TMPro;

// [REF]
//  デニッキ: TextMeshPro の文字だけでなくフォントも選択言語に応じて自動的に変えるエディタ拡張 https://xrdnk.hateblo.jp/entry/localized_textmeshpro_font
//  LIGHT11: あらゆる種類のアセットをローカライズできるようにする方法まとめ https://light11.hatenadiary.com/entry/2022/03/28/193708

namespace UnityEngine.Localization.Components {

    /// <summary>
    /// TmpFontAsset 用の LocalizedAssetEvent
    /// </summary>
    [AddComponentMenu("Localization/Asset/" + nameof(LocalizeTmpFontEvent))]
    public sealed class LocalizeTmpFontEvent : LocalizedAssetEvent<TMP_FontAsset, LocalizedTmpFont, UnityEventTmpFont> { }

    /// <summary>
    /// TmpFontAsset を引数とする Unity Event
    /// </summary>
    [Serializable]
    public class UnityEventTmpFont : UnityEvent<TMP_FontAsset> { }
}
