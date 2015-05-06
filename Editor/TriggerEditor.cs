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

            EditorGUILayout.PropertyField(
                mode,
                new GUIContent(
                    "Mode",
                    ""));


            EditorGUILayout.PropertyField(
                sourceGo,
                new GUIContent(
                    "Source Game Object",
                    ""));

            EditorGUILayout.PropertyField(
                targetObj,
                new GUIContent(
                    "Target Object",
                    ""));

            EditorGUILayout.PropertyField(
                triggerCount,
                new GUIContent(
                    "Trigger Count",
                    ""));

            EditorGUILayout.PropertyField(
                repeatTrigger,
                new GUIContent(
                    "Repeat Trigger",
                    ""));

            EditorGUILayout.PropertyField(
                action,
                new GUIContent(
                    "Action",
                    ""));

            serializedObject.ApplyModifiedProperties();
        }
    }

}
