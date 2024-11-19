using UnityEngine;

public class FadeController : MonoBehaviour
{
    public Animator fadeOutAnimator; // Reference to the fade-out Animator
    public float fadeOutDelay = 1f; // Delay for the fade-out animation

    private bool fadeTriggered = false; // Ensure the fade-out only happens once

    void Start()
    {
        // Automatically trigger the fade-out animation at the start of the main level
        TriggerFadeOut();
    }

    public void TriggerFadeOut()
    {
        if (!fadeTriggered)
        {
            StartCoroutine(PlayFadeOut());
        }
    }

    private System.Collections.IEnumerator PlayFadeOut()
    {
        fadeTriggered = true;

        // Wait briefly to show the new level
        yield return new WaitForSeconds(fadeOutDelay);

        // Trigger the fade-out animation
        if (fadeOutAnimator != null)
        {
            fadeOutAnimator.SetTrigger("FadeOut");
        }
    }
}
