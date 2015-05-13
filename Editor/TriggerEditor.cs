// Copyright (c) 2015 Bartlomiej Wolk (bartlomiejwolk@gmail.com)
// 
// This file is part of the ActionTrigger extension for Unity.
// Licensed under the MIT license. See LICENSE file in the project root folder.

using UnityEditor;
using UnityEngine;

namespace ActionTrigger {

    [CustomEditor(typeof (Trigger))]
    [CanEditMultipleObjects]
    public sealed class TriggerEditor : Editor {
        #region FIELDS

        private Trigger Script { get; set; }

        #endregion FIELDS

        #region METHODS

        [MenuItem("Component/ActionTrigger")]
        private static void AddWatcherComponent() {
            if (Selection.activeGameObject != null) {
                Selection.activeGameObject.AddComponent(typeof (Trigger));
            }
        }

        #endregion METHODS

        #region SERIALIZED PROPERTIES

        private SerializedProperty action;
        private SerializedProperty message;
        private SerializedProperty sourceGo;
        private SerializedProperty targetObj;
        private SerializedProperty triggerType;
        private SerializedProperty unityEventAction;
        private SerializedProperty description;
        private SerializedProperty includeTag;

        #endregion SERIALIZED PROPERTIES

        #region UNITY MESSAGES

        public override void OnInspectorGUI() {
            serializedObject.Update();

            DrawVersionLabel();
            DrawDescriptionTextArea();

            EditorGUILayout.Space();

            DrawTriggerDropdown();
            DrawIncludeTagDropdown();
            DrawModeDropdown();
            HandleDrawSourceGoField();
            HandleDrawTargetObjField();
            HandleDrawMessageField();
            HandleDrawActionField();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawIncludeTagDropdown() {
            if (Script.TriggerType != TriggerType.OnTriggerEnter) return;
            if (Script.TriggerType != TriggerType.OnTriggerExit) return;

            includeTag.stringValue = EditorGUILayout.TagField(
                new GUIContent(
                    "Tag",
                    "Only GOs with this tag can trigger action."),
                    includeTag.stringValue);
        }

        private void DrawDescriptionTextArea() {
            description.stringValue = EditorGUILayout.TextArea(
                description.stringValue);
        }

        private void OnEnable() {
            Script = (Trigger) target;

            triggerType = serializedObject.FindProperty("triggerType");
            action = serializedObject.FindProperty("action");
            targetObj = serializedObject.FindProperty("targetObj");
            sourceGo = serializedObject.FindProperty("sourceGo");
            unityEventAction = serializedObject.FindProperty("unityEventAction");
            message = serializedObject.FindProperty("message");
            description = serializedObject.FindProperty("description");
            includeTag = serializedObject.FindProperty("includeTag");
        }

        #endregion UNITY MESSAGES

        #region INSPECTOR
        private void DrawVersionLabel() {
            EditorGUILayout.LabelField(
                string.Format(
                    "{0} ({1})",
                    Trigger.VERSION,
                    Trigger.EXTENSION));
        }


        private void DrawModeDropdown() {
            EditorGUILayout.PropertyField(
                action,
                new GUIContent(
                    "Action",
                    "Action to perform when trigger condition is met."));
        }

        private void DrawTriggerDropdown() {
            EditorGUILayout.PropertyField(
                triggerType,
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

        private void HandleDrawMessageField() {
            if (Script.Action != Mode.Message) return;

            EditorGUILayout.PropertyField(
                message,
                new GUIContent(
                    "Message",
                    "Message to broadcast."));
        }

        private void HandleDrawSourceGoField() {
            if (Script.Action != Mode.Replace) return;

            EditorGUILayout.PropertyField(
                sourceGo,
                new GUIContent(
                    "Source Game Object",
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

        #endregion INSPECTOR
    }

}