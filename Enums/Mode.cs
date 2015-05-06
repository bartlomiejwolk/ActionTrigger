namespace ActionTrigger {

    /// <summary>
    /// A multi-purpose script which causes an action to occur on trigger enter.
    /// </summary>
    public enum Mode {

        UnityEvent, // Invoke UnityAction
        Message, // Just broadcast the action on to the target
        Enable, // Enable a component
        Activate, // Activate the target GameObject
        Deactivate, // Decativate target GameObject
        Replace, // replace target with source
        Animate // Start animation on target

    }

}