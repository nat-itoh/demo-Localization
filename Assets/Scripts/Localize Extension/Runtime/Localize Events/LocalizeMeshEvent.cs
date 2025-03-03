using System;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif

namespace UnityEngine.Localization.Components {


    [Serializable]
    public class LocalizedMesh : LocalizedAsset<Mesh> { }


    [Serializable]
    public class UnityEventMesh : UnityEvent<Mesh> { }


    /// <summary>
    /// <see cref="Mesh"/> 用の LocalizedAssetEvent.
    /// </summary>
    [AddComponentMenu("Localization/Asset/Localize Mesh Event")]
    public sealed class LocalizeMeshEvent : LocalizedAssetEvent<Mesh, LocalizedMesh, UnityEventMesh> {

#if UNITY_EDITOR
        [MenuItem("CONTEXT/MeshFilter/Localize")]
        private static void AttachAndSetupForMeshFilter(MenuCommand command) {
            var target = (MeshFilter)command.context;
            AttachAndSetupForMeshFilter(target);
        }

        public static LocalizeMeshEvent AttachAndSetupForMeshFilter(MeshFilter target) {
            var localizeEvent = (LocalizeMeshEvent)Undo.AddComponent(target.gameObject, typeof(LocalizeMeshEvent));

            // イベント発生時にsharedMeshが変更されるように
            var setMethod = typeof(MeshFilter).GetProperty("sharedMesh")?.GetSetMethod();
            if (setMethod != null) {
                var methodDelegate = (UnityAction<Mesh>)Delegate.CreateDelegate(typeof(UnityAction<Mesh>), target, setMethod);
                UnityEventTools.AddPersistentListener(localizeEvent.OnUpdateAsset, methodDelegate);
                localizeEvent.OnUpdateAsset.SetPersistentListenerState(0, UnityEventCallState.EditorAndRuntime);
            }

            return localizeEvent;
        }
#endif

    }

}
