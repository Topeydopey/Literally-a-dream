using UnityEngine;
using TMPro; // Use if you’re using TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro score display
    private int score = 0;

    void Awake()
    {
        // Singleton pattern to ensure only one instance of ScoreManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Attempt to find ScoreText in the current scene
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        }

        UpdateScoreText();
    }


    // Method to add score
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // Update the UI text display
    private void UpdateScoreText()
    {
        scoreText.text = "Things Stomped: " + score;
    }
}
