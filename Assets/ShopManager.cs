using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI pointsText; // Display player's points
    public int itemCost = 50; // Cost of an item

    private void Start()
    {
        UpdatePointsDisplay();
    }

    public void BuyItem()
    {
        int currentPoints = ScoreManager.instance.GetScore();

        if (currentPoints >= itemCost)
        {
            // Deduct points
            ScoreManager.instance.AddScore(-itemCost);

            // Provide the item (add logic for giving the item here)
            Debug.Log("Item purchased!");
        }
        else
        {
            Debug.Log("Not enough points!");
        }

        UpdatePointsDisplay();
    }

    private void UpdatePointsDisplay()
    {
        if (pointsText != null)
        {
            pointsText.text = "Points: " + ScoreManager.instance.GetScore();
        }
    }

    public void ReturnToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
}
