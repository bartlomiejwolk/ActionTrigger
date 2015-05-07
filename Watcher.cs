// This script was originally taken from standard Unity scripts package.

using UnityEngine;
using UnityEngine.Events;

namespace ActionTrigger {

    public enum Trigger { OnTriggerEnter, OnTriggerExit }

    /// <summary>
    /// Component that can trigger multiple types of action in response
    /// to multiple events, like OnTriggerEnter().
    /// </summary>
    public sealed class Watcher : MonoBehaviour {

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
        // todo extract methods
        public void PerformAction() {
            Object currentTarget = TargetObj != null ? TargetObj : gameObject;
            Behaviour targetBehaviour = currentTarget as Behaviour;
            GameObject targetGameObject = currentTarget as GameObject;
            if (targetBehaviour != null)
                targetGameObject = targetBehaviour.gameObject;

            switch (Action) {
                case Mode.UnityEvent:
                    UnityEventAction.Invoke();
                    break;
                case Mode.Message:
                    targetGameObject.BroadcastMessage(Message);
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