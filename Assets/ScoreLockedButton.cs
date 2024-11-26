using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreLockedButton : MonoBehaviour
{
    public Button lockedButton; // Reference to the button
    public AudioSource audioSource; // Reference to the AudioSource for playing sounds
    public AudioClip insufficientScoreSound; // Sound to play when the score is insufficient
    public AudioClip successSound; // Sound to play when the button is clicked successfully
    public int scoreThreshold = 500; // Minimum score required to unlock the button
    public string nextSceneName = "EndLevel"; // Name of the scene to load

    private void Start()
    {
        // Assign the OnClick event for the button
        lockedButton.onClick.AddListener(OnLockedButtonClicked);
    }

    private void OnLockedButtonClicked()
    {
        // Check if the player's score meets the threshold
        if (ScoreManager.instance != null && ScoreManager.instance.GetScore() >= scoreThreshold)
        {
            // Play success sound
            if (audioSource != null && successSound != null)
            {
                audioSource.PlayOneShot(successSound);
            }

            // Disable the button to prevent multiple clicks
            lockedButton.interactable = false;

            // Transition to the next scene
            StartCoroutine(LoadNextScene());
        }
        else
        {
            // Play insufficient score sound
            if (audioSource != null && insufficientScoreSound != null)
            {
                audioSource.PlayOneShot(insufficientScoreSound);
            }

            Debug.Log("Insufficient score to unlock this button!");
        }
    }

    private System.Collections.IEnumerator LoadNextScene()
    {
        // Wait for the sound to finish playing before transitioning
        if (successSound != null)
        {
            yield return new WaitForSeconds(successSound.length);
        }

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
