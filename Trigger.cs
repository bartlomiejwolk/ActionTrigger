// This script was originally taken from standard Unity scripts package.

using UnityEngine;
using UnityEngine.Events;

namespace ActionTrigger {

    public sealed class Trigger : MonoBehaviour {

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

        /// The action to accomplish
        public Mode mode = Mode.Activate;

        /// The game object to affect. If none, the trigger work on this game object
        public Object targetObj;

        public GameObject sourceGo;
        public int triggerCount = 1;

        ///
        public bool repeatTrigger = false;

        public UnityEvent action;

        private void DoActivateTrigger() {
            triggerCount--;

            if (triggerCount == 0 || repeatTrigger) {
                Object currentTarget = targetObj != null ? targetObj : gameObject;
                Behaviour targetBehaviour = currentTarget as Behaviour;
                GameObject targetGameObject = currentTarget as GameObject;
                if (targetBehaviour != null)
                    targetGameObject = targetBehaviour.gameObject;

                switch (mode) {
                    case Mode.UnityEvent:
                        action.Invoke();
                        break;
                    case Mode.Message:
                        targetGameObject.BroadcastMessage("DoActivateTrigger");
                        break;
                    case Mode.Replace:
                        if (sourceGo != null) {
                            Object.Instantiate(
                                sourceGo,
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

        private void OnTriggerEnter(Collider other) {
            DoActivateTrigger();
        }

    }

}