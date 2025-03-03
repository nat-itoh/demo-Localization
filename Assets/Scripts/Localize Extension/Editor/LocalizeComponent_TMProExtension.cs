# if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.Events;
using TMPro;
using UnityEngine.Events;

namespace UnityEngine.Localization.Components {

    /// <summary>
    /// TextMeshProを自動的にローカライズ設定するエディタ拡張
    /// </summary>
    internal static class LocalizeComponent_TMProExtension {
        
        [MenuItem("CONTEXT/TextMeshProUGUI/Localize With Font")]
        static void LocalizeTMProTextWithFontAssets(MenuCommand command) {
            var target = command.context as TextMeshProUGUI;
            SetupForLocalizeString(target);
            SetupForLocalizeTmpFont(target);
        }

        /// <summary>
        /// LocalizeStringEvent コンポーネントをアタッチすると同時に自動的に UpdateAsset イベントに text プロパティを変更する処理を追加する
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
        /// LocalizeTmpFontEvent コンポーネントをアタッチすると同時に自動的に UpdateAsset イベントに font プロパティを変更する処理を追加する
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