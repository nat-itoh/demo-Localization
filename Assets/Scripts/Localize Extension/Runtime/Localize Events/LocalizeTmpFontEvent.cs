using System;
using UnityEngine.Events;
using TMPro;

// [REF]
//  �f�j�b�L: TextMeshPro �̕��������łȂ��t�H���g���I������ɉ����Ď����I�ɕς���G�f�B�^�g�� https://xrdnk.hateblo.jp/entry/localized_textmeshpro_font
//  LIGHT11: �������ނ̃A�Z�b�g�����[�J���C�Y�ł���悤�ɂ�����@�܂Ƃ� https://light11.hatenadiary.com/entry/2022/03/28/193708

namespace UnityEngine.Localization
{
    [Serializable]
    public class UnityEventTmpFont : UnityEvent<TMP_FontAsset> { }
}


namespace UnityEngine.Localization.Components
{

    /// <summary>
    /// <see cref="TMP_FontAsset"/> 用のLocalizedAssetEvent．
    /// </summary>
    [AddComponentMenu("Localization/Asset/" + nameof(LocalizeTmpFontEvent))]
    public sealed class LocalizeTmpFontEvent : LocalizedAssetEvent<TMP_FontAsset, LocalizedTmpFont, UnityEventTmpFont> { }

}
