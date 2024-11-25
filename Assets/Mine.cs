using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mine : MonoBehaviour
{
    public GameObject explosionPrefab; // Reference to the explosion effect prefab
    public AudioClip explosionSound; // Explosion sound
    public int scorePenalty = 20; // Points to deduct when triggered

    public float blackoutDelay = 0.5f; // Delay before the blackout
    public float blackoutDuration = 1f; // Duration of the blackout fade
    public float transitionDelay = 2f; // Additional delay before transitioning to the shop
    public string shopSceneName = "ShopMenu"; // Name of the shop scene

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

        // Find the DeathScreen and BlackoutImage even if inactive
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("DeathScreen"))
            {
                deathScreen = obj;
                blackoutImage = deathScreen.GetComponentInChildren<Image>();
                break;
            }
        }

        if (deathScreen == null)
        {
            Debug.LogError("DeathScreen not found! Ensure it has the 'DeathScreen' tag and is in the scene.");
        }

        if (blackoutImage == null)
        {
            Debug.LogError("BlackoutImage not found! Ensure it is a child of the DeathScreen.");
        }

        // Ensure DeathScreen is inactive at start
        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cat"))
        {
            // Trigger the explosion
            Explode();

            // Reduce the player's score
            ScoreManager.instance.AddScore(-scorePenalty);

            // Trigger blackout and transition to the shop
            StartCoroutine(TriggerBlackoutAndTransition());
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

    private System.Collections.IEnumerator TriggerBlackoutAndTransition()
    {
        // Stop the music immediately
        AmbientMusicManager ambientManager = FindObjectOfType<AmbientMusicManager>();
        if (ambientManager != null)
        {
            ambientManager.FadeOutMusic();
        }

        // Wait for the blackout delay
        yield return new WaitForSeconds(blackoutDelay);

        // Activate the DeathScreen and start blackout
        if (deathScreen != null)
        {
            deathScreen.SetActive(true); // Activate DeathScreen
        }

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

        // Wait for the transition delay
        yield return new WaitForSeconds(transitionDelay);

        // Load the shop menu scene
        SceneManager.LoadScene(shopSceneName);
    }
}
