using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class AIController : MonoBehaviour
{
    public StateMachine statemachine;
    public Transform player;
    public NavMeshAgent agent;

    public float playerVolume = 15f;
    public Transform visionCone;
    public bool playerInCone;
    public bool canSeePlayer;
    public Transform[] patrolWaypoints;
    public int currentWaypointIndex;
    public float patrolSpeed = 5;

    public float detectionRange = 3;
    public float visionAngle = 90f;

    public float hearingRange = 15f;
    public float hearingThreshold = 10f;

    public bool isManaged = true;
    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        statemachine = new StateMachine();
        if (isManaged)
        {
            AIManager.Instance.RegisterAgent(this);
        }
        statemachine.ChangeState(new StatePatrol(this));
    }

    private void Update()
    {
        {
            statemachine.Update();
        }
    }

    public void ChangeState(State newState)
    {
        statemachine.ChangeState(newState);
    }

    public bool CanSeePlayer()
    {
        return HasLineOfSight(player);
        //return Vector3.Distance(transform.position, player.position) < detectionRange;
    }

    public bool CanHearPlayer(float noiseLevel)
    {
        if (player == null) return false;

        if(Vector3.Distance(transform.position, player.position) < hearingRange && noiseLevel > hearingThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
       
    public void SetPlayerInVisionCone(bool isVisible)
    {
        playerInCone = isVisible;
    }

    public bool HasLineOfSight(Transform target)
    {
        /* if (!playerInCone)
         {
             return false;
         }
         Vector3 directionToTarget = (target.position - transform.position).normalized;

         RaycastHit hit;
         if(Physics.Raycast(transform.position, directionToTarget, out hit, detectionRange)) 
         {
             if(hit.transform == target)
             {
                 return true; //player in in direct line of sight 
             }
         }
         return false;
        */
        if(Vector3.Distance(transform.position, player.position) <= 0.5f)
        {
            AIManager.Instance.lastKnownPlayerPos = player.position;
            return true;
        }

        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToTarget);

        if(angleToPlayer < visionAngle /2f)
        {
            Debug.Log("player in cone");

            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToTarget, out hit, detectionRange))
            {
                if (hit.transform == target)
                {
                    AIManager.Instance.lastKnownPlayerPos = player.position; 
                    return true; //player in in direct line of sight 
                }
            }
        }

        return false;
    }

    public void ChasePlayer()
    {
        /*transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * patrolSpeed);


        //rotatee the ai toward the next waypoint
        Vector3 direction = (player.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            float rotationSpeed = 3f; //rotation speed
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * patrolSpeed);

        if (Vector3.Distance(transform.position, player.position) < 0.2f) //check how close we are to target waypoint
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
        } */

        if(player == null || agent == null)
        {
            return;
        }
        if(!agent.pathPending && agent.destination != player.position)
        {
            agent.SetDestination(player.position);
        }
        
    }

    public void Patrol()
    {
        //patrol here
        if (patrolWaypoints.Length == 0)
        {
            return;
        }

       

        Transform targetWaypoint = patrolWaypoints[currentWaypointIndex];

        //rotatee the ai toward the next waypoint
        /*Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        if(direction != Vector3.zero)
        {
            float rotationSpeed = 3f; //rotation speed
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }*/

        //transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, Time.deltaTime * patrolSpeed);
        agent.SetDestination(targetWaypoint.position);


        /*if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.2f) //check how close we are to target waypoint
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
        }*/

        if (agent.remainingDistance < 0.2f) //check how close we are to target waypoint
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
            agent.SetDestination(patrolWaypoints[currentWaypointIndex].position);
        }

    }

}
