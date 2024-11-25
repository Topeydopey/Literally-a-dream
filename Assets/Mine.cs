using UnityEngine;
using UnityEngine.UI;

public class Mine : MonoBehaviour
{
    public GameObject explosionPrefab; // Reference to the explosion effect prefab
    public AudioClip explosionSound; // Explosion sound
    public int scorePenalty = 20; // Points to deduct when triggered

    public float blackoutDelay = 0.5f; // Delay before the blackout
    public float blackoutDuration = 1f; // Duration of the blackout fade

    private AudioSource audioSource;
    private Image blackoutImage; // Dynamically found at runtime
    private GameObject deathScreen; // Dynamically found at runtime

    private void Start()
    {
        // Add or get an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Get references from the GameManager
        blackoutImage = GameManager.instance?.blackoutImage;
        deathScreen = GameManager.instance?.deathScreen;

        if (blackoutImage == null)
        {
            Debug.LogError("BlackoutImage not found! Ensure it is assigned in the GameManager.");
        }

        if (deathScreen == null)
        {
            Debug.LogError("DeathScreen not found! Ensure it is assigned in the GameManager.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.PauseMovement();
        }

        if (other.CompareTag("Cat")) // Check if the colliding object is the cat
        {
            // Trigger the explosion
            Explode();

            // Reduce the player's score
            ScoreManager.instance.AddScore(-scorePenalty);

            // Trigger blackout and death screen
            StartCoroutine(TriggerBlackout());
        }
    }

    private void Explode()
    {
        // Instantiate explosion effect
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Play explosion sound
        if (explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }
    }

    private System.Collections.IEnumerator TriggerBlackout()
    {
        // Stop the music immediately
        AmbientMusicManager ambientManager = FindObjectOfType<AmbientMusicManager>();
        if (ambientManager != null)
        {
            ambientManager.FadeOutMusic(); // Initiate fade out
        }

        // Wait for the blackout delay
        yield return new WaitForSeconds(blackoutDelay);

        // Fade the screen to black
        if (blackoutImage != null)
        {
            float elapsedTime = 0f;
            Color initialColor = blackoutImage.color;
            Color targetColor = new Color(0, 0, 0, 1); // Full black

            while (elapsedTime < blackoutDuration)
            {
                elapsedTime += Time.deltaTime;
                blackoutImage.color = Color.Lerp(initialColor, targetColor, elapsedTime / blackoutDuration);
                yield return null;
            }
        }

        // Show the death screen
        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
        }
    }
}
