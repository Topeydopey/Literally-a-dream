using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance
    public int totalScore = 0; // The player's total score

    private ScoreManager scoreManager;

    private void Awake()
    {
        // Ensure there's only one GameManager instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist this object across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Find the ScoreManager in the scene
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene! Ensure it is present.");
        }
    }

    private void Update()
    {
        if (scoreManager != null)
        {
            // Sync totalScore with the ScoreManager's current score
            totalScore = scoreManager.GetScore();
        }
    }

    // Add points directly to the GameManager (if needed for global score handling)
    public void AddScore(int amount)
    {
        totalScore += amount;
        Debug.Log("Added " + amount + " points. Total Score: " + totalScore);
    }

    // Deduct points directly from the GameManager (useful for the shop)
    public bool DeductScore(int amount)
    {
        if (totalScore >= amount)
        {
            totalScore -= amount;
            Debug.Log("Deducted " + amount + " points. Total Score: " + totalScore);
            return true; // Deduction successful
        }
        Debug.Log("Not enough points to deduct. Current Score: " + totalScore);
        return false; // Not enough points
    }

    // Save the total score to PlayerPrefs
    public void SaveScore()
    {
        PlayerPrefs.SetInt("TotalScore", totalScore);
        PlayerPrefs.Save();
        Debug.Log("Score saved: " + totalScore);
    }

    // Load the total score from PlayerPrefs
    public void LoadScore()
    {
        if (PlayerPrefs.HasKey("TotalScore"))
        {
            totalScore = PlayerPrefs.GetInt("TotalScore");
            Debug.Log("Score loaded: " + totalScore);
        }
        else
        {
            Debug.Log("No saved score found. Starting at 0.");
        }
    }
}
