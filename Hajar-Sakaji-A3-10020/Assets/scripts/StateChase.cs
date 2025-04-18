using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChase : State
{
    // constructor — gives the AI reference to the base State class
    public StateChase(AIController ai) : base(ai)
    {
    }

    // called when entering chase state
    public override void Enter()
    {
        Debug.Log("Entering Chase State");
    }

    // runs every frame while in this state
    public override void Update()
    {
        ai.ChasePlayer(); // keep chasing the player

        // if the AI loses sight of the player, switch to search state
        if (!ai.CanSeePlayer())
        {
            ai.ChangeState(new StateSearchForPlayer(ai));
        }
    }

    // called when exiting chase state
    public override void Exit()
    {
        Debug.Log("Exiting Chase State");
    }
}
