using UnityEngine;

public class AmbientMusicManager : MonoBehaviour
{
    public AudioClip ambientMusic; // The music clip to play
    public float musicVolume = 0.5f; // Volume of the music (0 to 1)
    public float fadeOutDuration = 0.5f; // Time it takes to fade out the music

    private AudioSource audioSource;

    private void Awake()
    {
        // Ensure only one AmbientMusicManager exists in the scene
        if (FindObjectsOfType<AmbientMusicManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // Persist across scenes

        // Add or get an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure the AudioSource
        audioSource.clip = ambientMusic;
        audioSource.volume = musicVolume;
        audioSource.loop = true;
    }

    private void Start()
    {
        // Play the music if not already playing
        if (ambientMusic != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        musicVolume = Mathf.Clamp(volume, 0f, 1f);
        if (audioSource != null)
        {
            audioSource.volume = musicVolume;
        }
    }

    public void FadeOutMusic()
    {
        if (audioSource != null)
        {
            StartCoroutine(FadeOutCoroutine());
        }
    }

    private System.Collections.IEnumerator FadeOutCoroutine()
    {
        float startVolume = audioSource.volume;

        float elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeOutDuration);
            yield return null;
        }

        audioSource.Stop(); // Stop completely after fade out
        audioSource.volume = startVolume; // Reset volume for potential future use
    }
}
