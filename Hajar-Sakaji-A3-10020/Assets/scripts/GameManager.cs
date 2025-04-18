using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // global access to the GameManager
    public GameObject objectiveMessage; // UI message panel
    public int totalCans = 5; // how many necklaces the player needs (still called cans)
    private int cansCollected = 0; // keeps track of how many are collected

    private void Awake()
    {
        instance = this; // set the global reference
    }

    void Start()
    {
        // show the starting objective message
        ShowMessage("Objective: Steal the necklaces without getting caught...");
        Invoke("HideMessage", 5f); // hide it after 5 seconds
    }

    public void CanCollected()
    {
        cansCollected++; // increase the count

        // if the player got them all
        if (cansCollected >= totalCans)
        {
            ShowMessage("Great job! You collected all the necklaces! Go to the getaway car!");
            Invoke("HideMessage", 5f); // hide after a bit
        }

        // let the car know they can now escape
        FindObjectOfType<CarEscape>()?.EnableCarEscape();
    }

    public void ShowMessage(string message)
    {
        Debug.Log("Showing message: " + message);
        objectiveMessage.SetActive(true); // show the panel
        objectiveMessage.GetComponentInChildren<TextMeshProUGUI>().text = message; // update the text
    }

    public void HideMessage()
    {
        Debug.Log("Hiding message and panel");
        objectiveMessage.SetActive(false); // hide the panel
    }
}
