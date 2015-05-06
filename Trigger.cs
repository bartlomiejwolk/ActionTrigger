// This script was originally taken from standard Unity scripts package.

using UnityEngine;
using UnityEngine.Events;

namespace ActionTrigger {

    /// <summary>
    /// A multi-purpose script which causes an action to occur on trigger enter.
    /// </summary>
    public enum Mode {

        UnityEvent, // Invoke UnityAction
        Replace, // replace target with source
        Activate, // Activate the target GameObject
        Enable, // Enable a component
        Animate, // Start animation on target
        Deactivate, // Decativate target GameObject
        Message // Just broadcast the action on to the target

    }

    public sealed class Trigger : MonoBehaviour {

        #region FIELDS
        /// The action to accomplish
        public Mode mode = Mode.Activate;

        /// The game object to affect. If none, the trigger work on this game object
        public Object targetObj;

        public GameObject sourceGo;
        public int triggerCount = 1;

        ///
        public bool repeatTrigger = false;

        public UnityEvent action;
        #endregion

        #region PROPERTIES
        /// The action to accomplish
        public Mode Mode {
            get { return mode; }
            set { mode = value; }
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

        public int TriggerCount {
            get { return triggerCount; }
            set { triggerCount = value; }
        }

        ///
        public bool RepeatTrigger {
            get { return repeatTrigger; }
            set { repeatTrigger = value; }
        }

        public UnityEvent Action {
            get { return action; }
            set { action = value; }
        }

        #endregion

        #region UNITY MESSAGES
        private void OnTriggerEnter(Collider other) {
            DoActivateTrigger();
        }
        #endregion

        #region METHODS
        private void DoActivateTrigger() {
            TriggerCount--;

            if (TriggerCount == 0 || RepeatTrigger) {
                Object currentTarget = TargetObj != null ? TargetObj : gameObject;
                Behaviour targetBehaviour = currentTarget as Behaviour;
                GameObject targetGameObject = currentTarget as GameObject;
                if (targetBehaviour != null)
                    targetGameObject = targetBehaviour.gameObject;

                switch (Mode) {
                    case Mode.UnityEvent:
                        Action.Invoke();
                        break;
                    case Mode.Message:
                        targetGameObject.BroadcastMessage("DoActivateTrigger");
                        break;
                    case Mode.Replace:
                        if (SourceGo != null) {
                            Object.Instantiate(
                                SourceGo,
                                targetGameObject.transform.position,
                                targetGameObject.transform.rotation);
                            DestroyObject(targetGameObject);
                        }
                        break;
                    case Mode.Activate:
                        targetGameObject.SetActive(true);
                        break;
                    case Mode.Enable:
                        if (targetBehaviour != null)
                            targetBehaviour.enabled = true;
                        break;
                    case Mode.Animate:
                        targetGameObject.GetComponent<Animation>().Play();
                        break;
                    case Mode.Deactivate:
                        targetGameObject.SetActive(false);
                        break;
                }
            }
        }

        #endregion

    }

}