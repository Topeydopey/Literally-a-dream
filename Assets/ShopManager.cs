using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI totalScoreText; // Reference to the text element

    private void Update()
    {
        // Update the shop's UI with the player's total score
        totalScoreText.text = "Total Points: " + GameManager.instance.totalScore;
    }

    public void BuyItem(int cost)
    {
        if (GameManager.instance.DeductScore(cost))
        {
            Debug.Log("Purchase successful!");
        }
        else
        {
            Debug.Log("Not enough points to buy this item.");
        }

        // Update the score UI after a purchase
        totalScoreText.text = "Total Points: " + GameManager.instance.totalScore;
    }
}
