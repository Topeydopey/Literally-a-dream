using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Ensure this is included

public class Reel : MonoBehaviour
{
    public Sprite[] symbols; // Array of reel symbols
    public Image reelImage; // The Image component displaying the symbol
    public float spinSpeed = 0.1f; // Time between symbol changes

    private bool isSpinning = false;
    private Sprite finalSymbol;

    public void StartSpin()
    {
        isSpinning = true;
        StartCoroutine(Spin());
    }

    private IEnumerator Spin()
    {
        float spinDuration = Random.Range(2f, 3f); // Duration of spinning
        float elapsedTime = 0f;

        while (elapsedTime < spinDuration)
        {
            reelImage.sprite = symbols[Random.Range(0, symbols.Length)]; // Show random symbol
            elapsedTime += spinSpeed;
            yield return new WaitForSeconds(spinSpeed);
        }

        // Stop on a random final symbol
        finalSymbol = symbols[Random.Range(0, symbols.Length)];
        reelImage.sprite = finalSymbol;
        isSpinning = false;
    }

    public Sprite GetFinalSymbol()
    {
        return finalSymbol;
    }
}
