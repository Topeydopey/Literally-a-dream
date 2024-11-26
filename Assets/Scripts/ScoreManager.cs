using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton instance
    public TextMeshProUGUI scoreText; // Reference to the score text
    private int score = 0;

    [Header("References")]
    public DayNightCycleManager dayNightCycleManager; // Reference to the DayNightCycleManager

    // Rotation effect settings
    private float currentRotationIntensity = 0f; // Current rotation intensity
    public float maxRotationIntensity = 30f; // Maximum rotation intensity
    public float rotationIncreaseRate = 2f; // How quickly the rotation increases
    public float rotationDecreaseRate = 5f; // How quickly the rotation slows down
    public float baseRotationSpeed = 10f; // Speed of the seesaw rotation

    // Color effect settings
    public float baseColorChangeSpeed = 0.02f; // Base speed for the color change
    public float maxColorChangeMultiplier = 3f; // Maximum multiplier for the color speed
    public float colorChangeThreshold = 15f; // Threshold for rotation intensity to trigger color change

    private Color defaultColor = Color.white; // Default white color
    private bool isEffectActive = false; // Ensure effects only run when active

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    private void Start()
    {
        UpdateScoreText();
    }

    private void OnEnable()
    {
        // Listen to the sceneLoaded event to dynamically reassign scoreText
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Try to find the ScoreText object in the new scene
        if (scoreText == null)
        {
            GameObject scoreTextObject = GameObject.FindWithTag("ScoreText");
            if (scoreTextObject != null)
            {
                scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
                UpdateScoreText(); // Ensure the score is displayed correctly
            }
            else
            {
                Debug.LogError("ScoreText object not found in the new scene! Ensure it has the correct tag.");
            }
        }
    }
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        // Check for day-night cycle
        if (dayNightCycleManager != null)
        {
            dayNightCycleManager.CheckForDayNightCycle(score);
        }

        // Increase the rotation intensity
        currentRotationIntensity += rotationIncreaseRate;
        if (currentRotationIntensity > maxRotationIntensity)
        {
            currentRotationIntensity = maxRotationIntensity;
        }

        // Start the rotation and color effect if not already running
        if (!isEffectActive)
        {
            StartCoroutine(PlayEffects());
        }
    }

    public bool DeductScore(int amount)
    {
        if (score >= amount)
        {
            score -= amount;
            UpdateScoreText();
            return true; // Deduction successful
        }
        return false; // Not enough points
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Things Stomped: " + score;
    }

    private IEnumerator PlayEffects()
    {
        isEffectActive = true;
        float elapsedTime = 0f;

        while (currentRotationIntensity > 0)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the rotation angle using a sine wave for smooth oscillation
            float angle = Mathf.Sin(elapsedTime * baseRotationSpeed) * currentRotationIntensity;
            scoreText.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            // Apply color change only if the rotation intensity is above the threshold
            if (currentRotationIntensity >= colorChangeThreshold)
            {
                float colorSpeed = Mathf.Lerp(baseColorChangeSpeed, baseColorChangeSpeed * maxColorChangeMultiplier, currentRotationIntensity / maxRotationIntensity);
                float t = Mathf.PingPong(Time.time * colorSpeed, 1f);
                scoreText.color = Color.HSVToRGB(t, 1f, 1f);
            }
            else
            {
                // Reset to default color if below the threshold
                scoreText.color = defaultColor;
            }

            // Gradually reduce the rotation intensity
            currentRotationIntensity -= rotationDecreaseRate * Time.deltaTime;
            if (currentRotationIntensity < 0)
            {
                currentRotationIntensity = 0;
            }

            yield return null;
        }

        // Reset rotation and color after the effect ends
        scoreText.transform.rotation = Quaternion.identity;
        scoreText.color = defaultColor;
        isEffectActive = false;
    }
}
