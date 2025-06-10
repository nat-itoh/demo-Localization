using System;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif

namespace UnityEngine.Localization {

    [Serializable]
    public class UnityEventMesh : UnityEvent<Mesh> { }
}


namespace UnityEngine.Localization.Components
{
    /// <summary>
    /// <see cref="Mesh"/> 用のLocalizedAssetEvent.
    /// </summary>
    [AddComponentMenu("Localization/Asset/" + nameof(LocalizeMeshEvent))]
    public sealed class LocalizeMeshEvent : LocalizedAssetEvent<Mesh, LocalizedMesh, UnityEventMesh>
    {

#if UNITY_EDITOR
        [MenuItem("CONTEXT/MeshFilter/Localize")]
        private static void AttachAndSetupForMeshFilter(MenuCommand command)
        {
            var target = (MeshFilter)command.context;
            AttachAndSetupForMeshFilter(target);
        }

        public static LocalizeMeshEvent AttachAndSetupForMeshFilter(MeshFilter target)
        {
            var localizeEvent = (LocalizeMeshEvent)Undo.AddComponent(target.gameObject, typeof(LocalizeMeshEvent));

            // �C�x���g��������sharedMesh���ύX�����悤��
            var setMethod = typeof(MeshFilter).GetProperty("sharedMesh")?.GetSetMethod();
            if (setMethod != null)
            {
                var methodDelegate = (UnityAction<Mesh>)Delegate.CreateDelegate(typeof(UnityAction<Mesh>), target, setMethod);
                UnityEventTools.AddPersistentListener(localizeEvent.OnUpdateAsset, methodDelegate);
                localizeEvent.OnUpdateAsset.SetPersistentListenerState(0, UnityEventCallState.EditorAndRuntime);
            }

            return localizeEvent;
        }
#endif

    }

}
