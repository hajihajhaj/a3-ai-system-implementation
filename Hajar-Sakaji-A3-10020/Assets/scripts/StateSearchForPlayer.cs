using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSearchForPlayer : State
{
    private float searchTimer = 5f;
    private float currentSearchTime = 0f;
    private bool reachedSearchPosition = false;

    //constructor
    public StateSearchForPlayer(AIController ai) : base(ai)
    {
        
    }
    public override void Enter()
    {
        Debug.Log("Searching For Player");
        ai.agent.SetDestination(AIManager.Instance.lastKnownPlayerPos);
        reachedSearchPosition = false;
        currentSearchTime = 0;
    }

    public override void Update()
    {
        //check awareness

        //chase player if seen
        if (ai.CanSeePlayer())
        {
            ai.ChangeState(new StateChase(ai));
            return;
        }

        //travel to last known player position before searching/counting timer
        if (!reachedSearchPosition)
        {
            if(ai.agent.remainingDistance <= 1)
            {
                reachedSearchPosition = true;
            }
        }
        

        //spins around to look for player once they have reached last known player pos
        ai.transform.Rotate(Vector3.up * 60f * Time.deltaTime);
        Debug.Log("i should be spinning..");
        //go back to patrol after  5 seconds or so
        currentSearchTime += Time.deltaTime;
        if (currentSearchTime > searchTimer)
        {
            ai.ChangeState(new StatePatrol(ai));
        }
    }

    public override void Exit()
    {
        Debug.Log("");
    }

   
}
