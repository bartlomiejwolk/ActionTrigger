// Copyright (c) 2015 Bartlomiej Wolk (bartlomiejwolk@gmail.com)
// 
// This file is part of the ActionTrigger extension for Unity.
// Licensed under the MIT license. See LICENSE file in the project root folder.

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
        private SerializedProperty message;

        #endregion

        #region UNITY MESSAGES
        private void OnEnable() {
            Script = (Watcher) target;

            trigger = serializedObject.FindProperty("trigger");
            action = serializedObject.FindProperty("action");
            targetObj = serializedObject.FindProperty("targetObj");
            sourceGo = serializedObject.FindProperty("sourceGo");
            unityEventAction = serializedObject.FindProperty("unityEventAction");
            message = serializedObject.FindProperty("message");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            DrawVersionLabel();
            DrawTriggerDropdown();
            DrawModeDropdown();
            HandleDrawSourceGoField();
            HandleDrawTargetObjField();
            HandleDrawMessageField();
            HandleDrawActionField();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawVersionLabel() {
            EditorGUILayout.LabelField(string.Format(
                "{0} ({1})",
                Watcher.VERSION,
                Watcher.EXTENSION));
        }

        #endregion

        #region INSPECTOR
        private void HandleDrawMessageField() {
            EditorGUILayout.PropertyField(
                message,
                new GUIContent(
                    "Message",
                    "Message to broadcast."));
        }

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
