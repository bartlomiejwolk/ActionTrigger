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
        private SerializedProperty triggerCount;
        private SerializedProperty repeatTrigger;
        private SerializedProperty action;
        #endregion


        #region UNITY MESSAGES
        private void OnEnable() {
            Script = (Trigger) target;

            mode = serializedObject.FindProperty("mode");
            targetObj = serializedObject.FindProperty("targetObj");
            sourceGo = serializedObject.FindProperty("sourceGo");
            triggerCount = serializedObject.FindProperty("triggerCount");
            repeatTrigger = serializedObject.FindProperty("repeatTrigger");
            action = serializedObject.FindProperty("action");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            HandleDrawModeDropdown();
            HandleDrawSourceGoField();
            HandleDrawTargetObjField();
            HandleDrawTriggerCountField();
            HandleDrawRepeatTriggerToggle();
            HandleDrawActionField();

            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region INSPECTOR
        private void HandleDrawActionField() {
            EditorGUILayout.PropertyField(
                action,
                new GUIContent(
                    "Action",
                    ""));
        }

        private void HandleDrawRepeatTriggerToggle() {
            EditorGUILayout.PropertyField(
                repeatTrigger,
                new GUIContent(
                    "Repeat Trigger",
                    ""));
        }

        private void HandleDrawTriggerCountField() {
            EditorGUILayout.PropertyField(
                triggerCount,
                new GUIContent(
                    "Trigger Count",
                    ""));
        }

        private void HandleDrawTargetObjField() {
            EditorGUILayout.PropertyField(
                targetObj,
                new GUIContent(
                    "Target Object",
                    ""));
        }

        private void HandleDrawSourceGoField() {
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
