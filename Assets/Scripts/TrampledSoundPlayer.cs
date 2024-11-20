using UnityEngine;

public class TrampledSoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Despawn Settings")]
    public float despawnDelay = 5f; // Additional delay after the sound finishes

    private void Awake()
    {
        // Add or get an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume); // Use the passed volume

            // Destroy the object after the sound finishes plus the delay
            Destroy(gameObject, clip.length + despawnDelay);
        }
        else
        {
            // If no clip is provided, just destroy after the delay
            Destroy(gameObject, despawnDelay);
        }
    }
}
