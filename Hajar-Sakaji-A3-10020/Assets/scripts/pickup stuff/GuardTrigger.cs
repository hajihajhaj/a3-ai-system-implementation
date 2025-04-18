using UnityEngine;

public class GuardTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerPickup player = other.GetComponent<PlayerPickup>();
        if (player != null && player.hasJewelry)
        {
            Debug.Log("Caught by guard! Jewelry lost.");
            player.DropJewelry();
            // Add restart logic (e.g., reset position):
            other.transform.position = Vector3.zero;
        }
    }
}
