using UnityEngine;

public class TrampledSoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        // Add or get an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);

            // Optionally destroy the object after the sound finishes
            Destroy(gameObject, clip.length);
        }
    }
}
