using UnityEngine;

public class RainManager : MonoBehaviour
{
    [Header("Rain Settings")]
    public ParticleSystem rainEffect; // Particle system for rain
    public float minRainInterval = 5f; // Minimum time between rains
    public float maxRainInterval = 15f; // Maximum time between rains
    public float rainDuration = 5f; // How long the rain lasts

    [Header("Sound Settings")]
    public AudioClip rainSound; // Sound to play during rain
    public float fadeDuration = 1f; // Duration of the fade-in/out effect
    private AudioSource audioSource;

    private void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure the AudioSource
        audioSource.loop = true;
        audioSource.volume = 0f; // Start with volume at 0
        audioSource.playOnAwake = false;

        // Start the random rain cycle
        StartCoroutine(RandomRainCycle());
    }

    private System.Collections.IEnumerator RandomRainCycle()
    {
        while (true)
        {
            // Wait for a random interval before starting the rain
            float waitTime = Random.Range(minRainInterval, maxRainInterval);
            yield return new WaitForSeconds(waitTime);

            // Start the rain with fade-in
            StartRain();

            // Wait for the rain duration
            yield return new WaitForSeconds(rainDuration);

            // Stop the rain with fade-out
            StopRain();
        }
    }

    private void StartRain()
    {
        // Enable the rain effect
        if (rainEffect != null)
        {
            rainEffect.Play();
        }

        // Start the rain sound with fade-in
        if (rainSound != null)
        {
            audioSource.clip = rainSound;
            audioSource.Play();
            StartCoroutine(FadeAudio(1f, fadeDuration)); // Fade in to full volume
        }
    }

    private void StopRain()
    {
        // Disable the rain effect
        if (rainEffect != null)
        {
            rainEffect.Stop();
        }

        // Stop the rain sound with fade-out
        if (audioSource.isPlaying)
        {
            StartCoroutine(FadeAudio(0f, fadeDuration, true)); // Fade out and stop
        }
    }

    private System.Collections.IEnumerator FadeAudio(float targetVolume, float duration, bool stopAfterFade = false)
    {
        float startVolume = audioSource.volume;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;

        // Stop the audio source if fading out completely
        if (stopAfterFade && targetVolume == 0f)
        {
            audioSource.Stop();
        }
    }
}
