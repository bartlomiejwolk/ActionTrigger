using System.Security;
using UnityEditor;
using UnityEngine;

namespace ActionTrigger {

    [CustomEditor(typeof(Trigger))]
    sealed class TriggerEditor : Editor {

        #region FIELDS
        private Trigger Script { get; set; }

        #endregion

        #region SERIALIZED PROPERTIES

        private SerializedProperty mode;
        private SerializedProperty targetObj;
        private SerializedProperty sourceGo;
        private SerializedProperty action;
        #endregion


        #region UNITY MESSAGES
        private void OnEnable() {
            Script = (Trigger) target;

            mode = serializedObject.FindProperty("mode");
            targetObj = serializedObject.FindProperty("targetObj");
            sourceGo = serializedObject.FindProperty("sourceGo");
            action = serializedObject.FindProperty("action");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            HandleDrawModeDropdown();
            HandleDrawSourceGoField();
            HandleDrawTargetObjField();
            HandleDrawActionField();

            serializedObject.ApplyModifiedProperties();
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

        private void HandleDrawModeDropdown() {
            EditorGUILayout.PropertyField(
                mode,
                new GUIContent(
                    "Mode",
                    ""));
        }
#endregion

    }

}
