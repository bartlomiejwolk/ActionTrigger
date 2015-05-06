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

}