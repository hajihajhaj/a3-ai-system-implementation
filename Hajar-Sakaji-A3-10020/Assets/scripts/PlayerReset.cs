using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    public Transform spawnPoint; // where the player respawns

    private void OnTriggerEnter(Collider other)
    {
        // check if the thing that touched the trigger is the player
        if (other.CompareTag("Player"))
        {
            // send the player back to the spawn point
            other.transform.position = spawnPoint.position;

            // clear the player's collected items if they have the pickup script
            ObjectPickup pickup = other.GetComponent<ObjectPickup>();
            if (pickup != null)
                pickup.ClearInventory();

            // show the message that they got caught
            GameManager.instance.ShowMessage("You were caught!");
            Invoke("HideMessage", 5f); // hide it after 5 seconds

        }
    }

    private void HideMessage()
    {
        // hides the caught message
        GameManager.instance.HideMessage();
    }
}
