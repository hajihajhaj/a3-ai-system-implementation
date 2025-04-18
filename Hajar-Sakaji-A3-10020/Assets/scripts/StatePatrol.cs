using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrol : State
{
    // constructor - gives the base State class the AI reference
    public StatePatrol(AIController ai) : base(ai)
    {
    }

    // called when entering the patrol state
    public override void Enter()
    {
        Debug.Log("Entering Patrol State");
    }

    // runs every frame while in this state
    public override void Update()
    {
        // if the AI can see the player, switch to chase state
        if (ai.CanSeePlayer())
        {
            Debug.Log("Can See Player");
            ai.ChangeState(new StateChase(ai));
        }
        // if AI hears something but can't see the player, switch to search state
        else if (ai.CanHearPlayer(ai.playerVolume) && !ai.CanSeePlayer())
        {
            ai.ChangeState(new StateSearchForPlayer(ai));
        }
        else
        {
            // if nothing is detected, keep patrolling
            Debug.Log("Player line of sight lost");
            ai.Patrol();
        }
    }

    // called when leaving this state
    public override void Exit()
    {
        Debug.Log("Exiting Patrol State");
    }
}
