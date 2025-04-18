using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Camera cam;
    public float pickupRange = 5f;
    public bool hasJewelry = false;
    private GameObject heldJewelry;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
            {
                if (hit.collider.CompareTag("Jewelry"))
                {
                    heldJewelry = hit.collider.gameObject;
                    heldJewelry.SetActive(false); // Hide jewelry
                    hasJewelry = true;
                    Debug.Log("Jewelry picked up!");
                }
            }
        }
    }

    public void DropJewelry()
    {
        if (heldJewelry != null)
        {
            heldJewelry.SetActive(true);
            heldJewelry.transform.position = transform.position + transform.forward; // Drop in front of player
            heldJewelry = null;
        }
        hasJewelry = false;
    }
}
