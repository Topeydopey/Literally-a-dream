using UnityEngine;
using UnityEngine.Rendering.Universal; // Required for ShadowCaster2D

public class TrampleTree : MonoBehaviour
{
    public GameObject trampledVersion; // A version of the tree that appears after trampling
    public int scoreValue = 10; // Score value for trampling the tree
    public AudioClip[] trampleSounds; // Array of sounds to play when the tree is trampled
    public float soundVolume = 1f; // Volume of the trample sound (0 to 1)

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private ShadowCaster2D shadowCaster; // Reference to the ShadowCaster2D component
    private bool isTrampled = false; // Ensure trample logic runs only once

    private void Start()
    {
        // Add or get an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the ShadowCaster2D component
        shadowCaster = GetComponent<ShadowCaster2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cat") && !isTrampled)
        {
            isTrampled = true; // Mark as trampled to prevent duplicate execution
            Trample();
        }
    }

    private void Trample()
    {
        // Hide the original sprite
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false; // Make the sprite invisible
        }

        // Disable the ShadowCaster2D component
        if (shadowCaster != null)
        {
            shadowCaster.enabled = false; // Disable shadow casting
        }

        // Instantiate the trampled version of the tree
        if (trampledVersion != null)
        {
            Instantiate(trampledVersion, transform.position, transform.rotation);
        }

        // Choose a random sound to play
        if (trampleSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, trampleSounds.Length);
            AudioClip randomSound = trampleSounds[randomIndex];

            // Play the random sound with specified volume
            audioSource.PlayOneShot(randomSound, soundVolume);

            // Destroy the tree after the sound finishes playing
            Destroy(gameObject, randomSound.length);
        }
        else
        {
            // If no sound is set, destroy immediately
            Destroy(gameObject);
        }

        // Add score when the tree is trampled
        ScoreManager.instance.AddScore(scoreValue);
    }
}
