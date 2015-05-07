// Copyright (c) 2015 Bartlomiej Wolk (bartlomiejwolk@gmail.com)
// 
// This file is part of the ActionTrigger extension for Unity.
// Licensed under the MIT license. See LICENSE file in the project root folder.

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