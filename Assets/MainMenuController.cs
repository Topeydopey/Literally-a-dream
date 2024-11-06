using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button startButton;
    public TunnelLooper[] tunnelSegments; // Array of TunnelLooper segments
    public GameObject tunnelEndSegment; // Reference to the tunnel end sprite
    public float transitionDelay = 2f; // Delay before loading the main level

    void Start()
    {
        // Assign the OnClick event
        startButton.onClick.AddListener(OnStartButtonClicked);
        tunnelEndSegment.SetActive(false); // Initially hide the tunnel end segment
    }

    void OnStartButtonClicked()
    {
        // Stop the tunnel loop for each segment
        foreach (TunnelLooper segment in tunnelSegments)
        {
            segment.StopLooping();
        }

        // Show the tunnel end segment
        tunnelEndSegment.SetActive(true);

        // Start the transition to the main level
        StartCoroutine(TransitionToMainLevel());
    }

    System.Collections.IEnumerator TransitionToMainLevel()
    {
        // Wait for the transition delay to finish
        yield return new WaitForSeconds(transitionDelay);

        // Load the main level scene
        SceneManager.LoadScene("MainLevel");
    }
}
