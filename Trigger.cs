// This script was originally taken from standard Unity scripts package.

using UnityEngine;
using UnityEngine.Events;

namespace ActionTrigger {

    public sealed class Trigger : MonoBehaviour {

        #region FIELDS
        /// The action to accomplish
        public Mode mode = Mode.Activate;

        /// The game object to affect. If none, the trigger work on this game object
        public Object targetObj;

        public GameObject sourceGo;

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
                        Instantiate(
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

        #endregion

    }

}