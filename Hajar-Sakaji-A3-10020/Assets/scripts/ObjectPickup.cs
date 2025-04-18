using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectPickup : MonoBehaviour
{
    public Camera cam; // player's camera
    public float pickupRange = 3f; // how far the player can pick stuff up
    public TextMeshProUGUI pickupText; // shows the pickup prompt
    public TextMeshProUGUI inventoryCountText; // shows how many items collected

    private List<GameObject> inventory = new List<GameObject>(); // keeps track of picked-up items
    private GameObject currentItem; // the item currently being looked at
    private bool canPickup = false; // whether an item is in range and ready to be picked up

    void Update()
    {
        // check if the player is looking at a pickup item
        if (Physics.SphereCast(cam.transform.position, 0.5f, cam.transform.forward, out RaycastHit hit, pickupRange))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                currentItem = hit.collider.gameObject;
                pickupText.gameObject.SetActive(true); // show prompt
                canPickup = true;
            }
            else
            {
                pickupText.gameObject.SetActive(false); // hide prompt
                canPickup = false;
            }
        }
        else
        {
            pickupText.gameObject.SetActive(false);
            canPickup = false;
        }

        // if player clicks and can pick something up
        if (Input.GetMouseButtonDown(0) && canPickup && currentItem != null)
        {
            PickupObject(currentItem);
        }
    }

    void PickupObject(GameObject obj)
    {
        // only add if it's not already in inventory
        if (!inventory.Contains(obj))
        {
            inventory.Add(obj);
            obj.SetActive(false); // hide the object
            UpdateUI(); // update the count
            GameManager.instance.CanCollected(); // let GameManager know we picked something up
        }
    }

    void UpdateUI()
    {
        int count = inventory.Count;

        // update the on-screen number
        if (inventoryCountText != null)
            inventoryCountText.text = count.ToString();
    }

    public void ClearInventory()
    {
        inventory.Clear(); // empty the list
        UpdateUI(); // reset the counter
    }
}
