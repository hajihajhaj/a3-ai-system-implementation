using UnityEngine;
using TMPro;

public class MessageManager : MonoBehaviour
{
    public static MessageManager instance; // lets other scripts access this easily
    public TextMeshProUGUI messageText; // reference to the on-screen message text
    public float messageDuration = 3f; // how long the message stays before hiding

    private void Awake()
    {
        instance = this; // set the static instance so other scripts can use it
    }

    public void ShowMessage(string text)
    {
        CancelInvoke(); // stop any previous hide timers
        messageText.text = text; // set the message
        messageText.gameObject.SetActive(true); // show it
        Invoke(nameof(HideMessage), messageDuration); // hide it after a few seconds
    }

    void HideMessage()
    {
        messageText.gameObject.SetActive(false); // hides the message
    }
}
