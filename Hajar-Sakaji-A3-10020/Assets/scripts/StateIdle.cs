using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State
{
    // constructor — passes the AI reference to the base State class
    public StateIdle(AIController ai) : base(ai)
    {
    }

    // called when entering idle state
    public override void Enter()
    {
        Debug.Log("Entering Idle State");
    }

    // runs every frame while in idle state
    public override void Update()
    {
        // if the AI sees the player, switch to chase
        if (ai.CanSeePlayer())
        {
            ai.ChangeState(new StateChase(ai));
        }
    }

    // called when exiting idle state
    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}
