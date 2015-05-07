// Copyright (c) 2015 Bartlomiej Wolk (bartlomiejwolk@gmail.com)
// 
// This file is part of the ActionTrigger extension for Unity.
// Licensed under the MIT license. See LICENSE file in the project root folder.

using UnityEngine;
using UnityEngine.Events;

namespace ActionTrigger {

    /// <summary>
    /// Component that can trigger multiple types of action in response
    /// to multiple events, like OnTriggerEnter().
    /// </summary>
    /// <remarks>
    /// This script was originally part of standard Unity scripts package.
    /// </remarks>
    public sealed class Watcher : MonoBehaviour {

        #region CONSTANTS

        public const string VERSION = "v0.1.0";
        public const string EXTENSION = "ActionTrigger";

        #endregion

        #region FIELDS

        [SerializeField]
        private Trigger trigger;

        /// The action to accomplish
        [SerializeField]
        private Mode action;

        /// The game object to affect. If none, the trigger work on this game object
        [SerializeField]
        private Object targetObj;

        [SerializeField]
        private GameObject sourceGo;

        [SerializeField]
        private UnityEvent unityEventAction;

        [SerializeField]
        private string message;

        #endregion

        #region PROPERTIES
        /// The action to accomplish
        public Mode Action {
            get { return action; }
            set { action = value; }
        }

        /// The game object to affect. If none, the trigger work on this game object
        public Object TargetObj {
            get { return targetObj; }
            set { targetObj = value; }
        }

        public GameObject SourceGo {
            get { return sourceGo; }
            set { sourceGo = value; }
        }

        public UnityEvent UnityEventAction {
            get { return unityEventAction; }
            set { unityEventAction = value; }
        }

        public Trigger Trigger {
            get { return trigger; }
            set { trigger = value; }
        }

        public string Message {
            get { return message; }
            set { message = value; }
        }

        #endregion

        #region UNITY MESSAGES
        private void OnTriggerEnter(Collider other) {
            if (Trigger != Trigger.OnTriggerEnter) return;

            PerformAction();
        }

        private void OnTriggerExit() {
            if (Trigger != Trigger.OnTriggerExit) return;

            PerformAction();
        }

        #endregion

        #region METHODS
        public void PerformAction() {
            // Get Object.
            var currentTarget = TargetObj ?? gameObject;

            // Convert Object to Behaviour.
            // Will be null if GameObject was passed.
            var targetBehaviour = currentTarget as Behaviour;
            // Convert Object to GameObject.
            // Will be null if Behaviour was passed.
            var targetGameObject = currentTarget as GameObject;

            // If component was passed..
            if (targetBehaviour != null) {
                // Get its GameObject.
                targetGameObject = targetBehaviour.gameObject;
            }

            switch (Action) {
                case Mode.UnityEvent:
                    UnityEventAction.Invoke();
                    break;
                case Mode.Message:
                    targetGameObject.BroadcastMessage(Message);
                    break;
                case Mode.Replace:
                    HandleReplaceMode(targetGameObject);
                    break;
                case Mode.Activate:
                    targetGameObject.SetActive(true);
                    break;
                case Mode.Enable:
                    HandleEnableMode(targetBehaviour);
                    break;
                case Mode.Animate:
                    targetGameObject.GetComponent<Animation>().Play();
                    break;
                case Mode.Deactivate:
                    targetGameObject.SetActive(false);
                    break;
            }
        }

        private static void HandleEnableMode(Behaviour targetBehaviour) {
            if (targetBehaviour == null) return;

            targetBehaviour.enabled = true;
        }

        private void HandleReplaceMode(GameObject targetGameObject) {
            if (SourceGo == null) return;

            Instantiate(
                SourceGo,
                targetGameObject.transform.position,
                targetGameObject.transform.rotation);
            DestroyObject(targetGameObject);
        }

        #endregion

    }

}