using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CarEscape : MonoBehaviour
{
    public Camera playerCamera; // the player's camera
    public float interactRange = 3f; // how close the player has to be to interact
    public TextMeshProUGUI interactText; // the text that pops up when looking at the car
    public string sceneToLoad = "WinScene"; // name of the scene to load

    private bool canEscape = false; // only becomes true after all jewelry is collected

    void Update()
    {
        if (!canEscape) return; // don’t do anything unless escape is allowed

        // shoot a ray forward from the camera
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            // if the ray hits this car
            if (hit.collider.gameObject == gameObject)
            {
                // show the escape message
                interactText.gameObject.SetActive(true);
                interactText.text = "Click to escape";

                // if player clicks left mouse
                if (Input.GetMouseButtonDown(0))
                {
                    // go to win scene
                    SceneManager.LoadScene(sceneToLoad);
                }

                return;
            }
        }

        // hide the text if not looking at the car
        interactText.gameObject.SetActive(false);
    }

    // called when player collects everything
    public void EnableCarEscape()
    {
        canEscape = true;
    }
}
