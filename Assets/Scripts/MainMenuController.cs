using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Animator fadeOutAnimator; // Reference to the fade-out animator
    public AudioSource audioSource; // Reference to the AudioSource
    public Button startButton; // Reference to the Start button
    public float transitionDelay = 1f; // Delay after fade-out before loading the next scene

    void Start()
    {
        // Assign the OnClick event for the start button
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    public void OnStartButtonClicked()
    {
        Debug.Log("Button Pressed");
        // Play the sound effect
        audioSource.Play();

        // Trigger the fade-out animation
        fadeOutAnimator.SetTrigger("FadeOut");

        // Start the coroutine to load the main level after a delay
        StartCoroutine(LoadMainLevel());
    }

    System.Collections.IEnumerator LoadMainLevel()
    {
        // Wait for the transition delay to finish
        yield return new WaitForSeconds(transitionDelay);

        // Load the main level scene
        SceneManager.LoadScene("MainLevel");
    }
}
