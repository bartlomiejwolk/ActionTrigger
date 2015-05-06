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
        private SerializedProperty mode;
        private SerializedProperty targetObj;
        private SerializedProperty sourceGo;
        private SerializedProperty action;
        #endregion

        #region UNITY MESSAGES
        private void OnEnable() {
            Script = (Watcher) target;

            trigger = serializedObject.FindProperty("trigger");
            mode = serializedObject.FindProperty("mode");
            targetObj = serializedObject.FindProperty("targetObj");
            sourceGo = serializedObject.FindProperty("sourceGo");
            action = serializedObject.FindProperty("action");
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

        private void DrawTriggerDropdown() {
            EditorGUILayout.PropertyField(
                trigger,
                new GUIContent(
                    "Trigger",
                    ""));
        }


        #endregion

        #region INSPECTOR
        private void HandleDrawActionField() {
            if (Script.Mode != Mode.UnityEvent) return;

            EditorGUILayout.PropertyField(
                action,
                new GUIContent(
                    "Action",
                    ""));
        }

        private void HandleDrawTargetObjField() {
            if (Script.Mode == Mode.UnityEvent) return;

            EditorGUILayout.PropertyField(
                targetObj,
                new GUIContent(
                    "Target Object",
                    ""));
        }

        private void HandleDrawSourceGoField() {
            if (Script.Mode != Mode.Replace) return;

            EditorGUILayout.PropertyField(
                sourceGo,
                new GUIContent(
                    "Source Game Object",
                    ""));
        }

        private void DrawModeDropdown() {
            EditorGUILayout.PropertyField(
                mode,
                new GUIContent(
                    "Action",
                    ""));
        }
#endregion

    }

}
