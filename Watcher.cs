﻿// Copyright (c) 2015 Bartlomiej Wolk (bartlomiejwolk@gmail.com)
// 
// This file is part of the ActionTrigger extension for Unity.
// Licensed under the MIT license. See LICENSE file in the project root folder.

using UnityEngine;
using UnityEngine.Events;

namespace ActionTrigger {

    /// <summary>
    ///     Component that can trigger multiple types of action in response to
    ///     multiple events, like OnTriggerEnter().
    /// </summary>
    /// <remarks>
    ///     This script was originally part of standard Unity scripts package.
    /// </remarks>
    public sealed class Watcher : MonoBehaviour {
        #region CONSTANTS

        public const string EXTENSION = "ActionTrigger";
        public const string VERSION = "v0.1.0";

        #endregion CONSTANTS

        #region FIELDS

        /// The action to accomplish
        [SerializeField]
        private Mode action;

        [SerializeField]
        private string message;

        [SerializeField]
        private GameObject sourceGo;

        /// The game object to affect. If none, the trigger work on this game
        /// object
        [SerializeField]
        private Object targetObj;

        [SerializeField]
        private Trigger trigger;

        [SerializeField]
        private UnityEvent unityEventAction;

        #endregion FIELDS

        #region PROPERTIES

        /// The action to accomplish
        public Mode Action {
            get { return action; }
            set { action = value; }
        }

        public string Message {
            get { return message; }
            set { message = value; }
        }

        public GameObject SourceGo {
            get { return sourceGo; }
            set { sourceGo = value; }
        }

        /// The game object to affect. If none, the trigger work on this game
        /// object
        public Object TargetObj {
            get { return targetObj; }
            set { targetObj = value; }
        }

        public Trigger Trigger {
            get { return trigger; }
            set { trigger = value; }
        }

        public UnityEvent UnityEventAction {
            get { return unityEventAction; }
            set { unityEventAction = value; }
        }

        #endregion PROPERTIES

        #region UNITY MESSAGES

        private void OnTriggerEnter(Collider other) {
            if (Trigger != Trigger.OnTriggerEnter) return;

            PerformAction();
        }

        private void OnTriggerExit() {
            if (Trigger != Trigger.OnTriggerExit) return;

            PerformAction();
        }

        #endregion UNITY MESSAGES

        #region METHODS

        public void PerformAction() {
            // Get Object.
            var currentTarget = TargetObj ?? gameObject;

            // Convert Object to Behaviour. Will be null if GameObject was
            // passed.
            var targetBehaviour = currentTarget as Behaviour;
            // Convert Object to GameObject. Will be null if Behaviour was
            // passed.
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
                    HandleMessageMode(targetGameObject);
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

        private void HandleMessageMode(GameObject targetGameObject) {
            if (targetGameObject == null) {
                Utilities.MissingReference(
                    this,
                    "Target Object",
                    "You didn't specify GameObject to send the message to.",
                    InfoType.Warning);

                return;
            }

            targetGameObject.BroadcastMessage(Message);
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

        #endregion METHODS
    }

}