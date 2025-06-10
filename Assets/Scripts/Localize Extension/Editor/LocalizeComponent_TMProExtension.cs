# if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using UnityEditor.Events;
using TMPro;

namespace UnityEngine.Localization.Components {

    /// <summary>
    /// TextMeshPro�������I�Ƀ��[�J���C�Y�ݒ肷��G�f�B�^�g��
    /// </summary>
    internal static class LocalizeComponent_TMProExtension {
        
        [MenuItem("CONTEXT/TextMeshProUGUI/Localize With Font")]
        private static void LocalizeTMProTextWithFontAssets(MenuCommand command) {
            var target = command.context as TextMeshProUGUI;
            SetupForLocalizeString(target);
            SetupForLocalizeTmpFont(target);
        }

        /// <summary>
        /// LocalizeStringEvent �R���|�[�l���g���A�^�b�`����Ɠ����Ɏ����I�� UpdateAsset �C�x���g�� text �v���p�e�B��ύX���鏈����ǉ�����
        /// </summary>
        /// <param name="target">TextMeshProUGUI</param>
        private static void SetupForLocalizeString(TextMeshProUGUI target) {
            var comp = Undo.AddComponent(target.gameObject, typeof(LocalizeStringEvent)) as LocalizeStringEvent;
            var setStringMethod = target.GetType().GetProperty("text").GetSetMethod();
            var methodDelegate = Delegate.CreateDelegate(typeof(UnityAction<string>), target, setStringMethod) as UnityAction<string>;
            
            UnityEventTools.AddPersistentListener(comp.OnUpdateString, methodDelegate);
            comp.OnUpdateString.SetPersistentListenerState(0, UnityEventCallState.EditorAndRuntime);
        }

        /// <summary>
        /// LocalizeTmpFontEvent �R���|�[�l���g���A�^�b�`����Ɠ����Ɏ����I�� UpdateAsset �C�x���g�� font �v���p�e�B��ύX���鏈����ǉ�����
        /// </summary>
        /// <param name="target">TextMeshProUGUI</param>
        private static void SetupForLocalizeTmpFont(TextMeshProUGUI target) {

            var comp = Undo.AddComponent(target.gameObject, typeof(LocalizeTmpFontEvent)) as LocalizeTmpFontEvent;
            var setStringMethod = target.GetType().GetProperty("font").GetSetMethod();
            var methodDelegate = Delegate.CreateDelegate(typeof(UnityAction<TMP_FontAsset>), target, setStringMethod) as UnityAction<TMP_FontAsset>;

            UnityEventTools.AddPersistentListener(comp.OnUpdateAsset, methodDelegate);
            comp.OnUpdateAsset.SetPersistentListenerState(0, UnityEventCallState.EditorAndRuntime);
        }
    }
}
#endif