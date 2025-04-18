using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This assumes we are using a mesh collider for the vision cone
/// </summary>
public class VisionConeTrigger : MonoBehaviour
{
    private AIController ai;

    private void Start()
    {
        ai = GetComponentInParent<AIController>(); // get the AIController from the parent object
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the player enters the cone
        if (other.CompareTag("Player"))
        {
            Debug.Log("player in vision cone");
            ai.SetPlayerInVisionCone(true); // tell AI the player is visible
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // if the player leaves the cone
        if (other.CompareTag("Player"))
        {
            Debug.Log("player isn't in vision cone");
            ai.SetPlayerInVisionCone(false); // tell AI the player is no longer visible
        }
    }
}
