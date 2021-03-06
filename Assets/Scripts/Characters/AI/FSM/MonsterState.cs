﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class MonsterState : ScriptableObject
{
    /****************************************************
     *
     * Inspector related objects
     *
     ****************************************************/

    // Inspector variables
    public TransitionPack[] Transitions;
    public MonsterAction[] Actions;

    public StateEvent[] BeginEvents;
    public StateEvent[] EndEvents;

    /// <summary>
    /// Function to be call at the beginning of this state
    /// </summary>
    /// <param name="msc">The monster state controller to be used</param>
    public abstract void Begin(MonsterStateController msc);

    /// <summary>
    /// Used to update the state based on the actions/transitions
    /// provided by the client
    /// </summary>
    /// <param name="msc">The monster state controller to be used</param>
    public abstract void UpdateState(MonsterStateController msc);

    /// <summary>
    /// Function to be call at the end of this state
    /// </summary>
    /// <param name="msc">The monster state controller to be used</param>
    public abstract void End(MonsterStateController msc);
}
