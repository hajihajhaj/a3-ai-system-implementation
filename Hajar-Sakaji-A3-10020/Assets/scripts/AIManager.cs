using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{

    public static AIManager Instance {  get; private set; }

    //player spotted
    //list of agents that want to make use of shared alerts 
    //registerd agents

    public Vector3 lastKnownPlayerPos;
    //last known player pos
    public List<AIController> registeredAgents = new List<AIController>();

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void RegisterAgent(AIController ai)
    {
        if(ai.isManaged)
        {
            registeredAgents.Add(ai);
        }
    }

    public void UnregisterAgent(AIController ai)
    {
        if (ai.isManaged)
        {
            registeredAgents.Remove(ai);
        }
    }

    //alert player spotted

    public void AlertPlayerSpotted()
    {
        //player spotted = true
        //update player position

        //notify all managed agents
        foreach (var ai in registeredAgents)
        {
            ai.ChangeState(new StateSearchForPlayer(ai));
        }
    }
}
