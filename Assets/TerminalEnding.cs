using UnityEngine;
using TMPro;

public class TerminalEndScreen : MonoBehaviour
{
    public TextMeshProUGUI terminalText; // Reference to the terminal text
    public string[] endMessages; // Array of messages to display
    public float typingSpeed = 0.05f; // Time between each character
    public AudioClip endSound; // Sound to play at the end of typing
    private AudioSource audioSource; // Audio source for playing sounds

    private bool isTypingComplete = false;

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        StartCoroutine(DisplayEndMessages());
    }

    private System.Collections.IEnumerator DisplayEndMessages()
    {
        // Loop through each message
        foreach (string message in endMessages)
        {
            // Type out each character in the message
            foreach (char c in message)
            {
                terminalText.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }
            terminalText.text += "\n"; // Add a new line after each message
        }

        // Typing complete
        isTypingComplete = true;

        // Play the end sound
        if (endSound != null)
        {
            audioSource.PlayOneShot(endSound);
        }

        // Log the completion for debugging
        Debug.Log("All messages have been typed. Exiting the game in 2 seconds...");

        // Wait 2 seconds and exit the application
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}
