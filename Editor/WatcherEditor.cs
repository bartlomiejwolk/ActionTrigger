using UnityEditor;
using UnityEngine;

namespace ActionTrigger {

    [CustomEditor(typeof(Watcher))]
    public sealed class WatcherEditor : Editor {

        #region FIELDS
        private Watcher Script { get; set; }

        #endregion

        #region SERIALIZED PROPERTIES

        private SerializedProperty trigger;
        private SerializedProperty action;
        private SerializedProperty targetObj;
        private SerializedProperty sourceGo;
        private SerializedProperty unityEventAction;
        #endregion

        #region UNITY MESSAGES
        private void OnEnable() {
            Script = (Watcher) target;

            trigger = serializedObject.FindProperty("trigger");
            action = serializedObject.FindProperty("action");
            targetObj = serializedObject.FindProperty("targetObj");
            sourceGo = serializedObject.FindProperty("sourceGo");
            unityEventAction = serializedObject.FindProperty("unityEventAction");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            DrawTriggerDropdown();
            DrawModeDropdown();
            HandleDrawSourceGoField();
            HandleDrawTargetObjField();
            HandleDrawActionField();

            serializedObject.ApplyModifiedProperties();
        }
        #endregion

        #region INSPECTOR
        private void DrawTriggerDropdown() {
            EditorGUILayout.PropertyField(
                trigger,
                new GUIContent(
                    "Trigger",
                    "Event that triggers the action."));
        }

        private void HandleDrawActionField() {
            if (Script.Action != Mode.UnityEvent) return;

            EditorGUILayout.PropertyField(
                unityEventAction,
                new GUIContent(
                    "Action",
                    ""));
        }

        private void HandleDrawTargetObjField() {
            if (Script.Action == Mode.UnityEvent) return;

            EditorGUILayout.PropertyField(
                targetObj,
                new GUIContent(
                    "Target Object",
                    ""));
        }

        private void HandleDrawSourceGoField() {
            if (Script.Action != Mode.Replace) return;

            EditorGUILayout.PropertyField(
                sourceGo,
                new GUIContent(
                    "Source Game Object",
                    ""));
        }

        private void DrawModeDropdown() {
            EditorGUILayout.PropertyField(
                action,
                new GUIContent(
                    "Action",
                    "Action to perform when trigger condition is met."));
        }
#endregion

        #region METHODS

        [MenuItem("Component/ActionTrigger")]
        private static void AddWatcherComponent() {
            if (Selection.activeGameObject != null) {
                Selection.activeGameObject.AddComponent(typeof (Watcher));
            }
        }


        #endregion

    }

}
