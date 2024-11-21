using UnityEngine;
using System.Collections;

public class SlotMachineManager : MonoBehaviour
{
    public Reel[] reels; // Array of Reel components
    public GameObject rewardsPanel; // UI panel to display rewards

    public void SpinReels()
    {
        // Start all reels spinning
        foreach (var reel in reels)
        {
            reel.StartSpin();
        }

        StartCoroutine(CheckResults());
    }

    private IEnumerator CheckResults()
    {
        // Wait for all reels to stop spinning
        yield return new WaitUntil(() => AllReelsStopped());

        // Check the final symbols on the reels
        Sprite[] results = new Sprite[reels.Length];
        for (int i = 0; i < reels.Length; i++)
        {
            results[i] = reels[i].GetFinalSymbol();
        }

        // Determine if the player wins and show rewards
        ProcessResults(results);
    }

    private bool AllReelsStopped()
    {
        foreach (var reel in reels)
        {
            if (reel.isActiveAndEnabled) return false;
        }
        return true;
    }

    private void ProcessResults(Sprite[] results)
    {
        // Example: Check if all symbols match
        bool isWin = true;
        for (int i = 1; i < results.Length; i++)
        {
            if (results[i] != results[0])
            {
                isWin = false;
                break;
            }
        }

        // Show rewards or a "try again" message
        if (isWin)
        {
            Debug.Log("You win!");
            rewardsPanel.SetActive(true); // Show reward panel
        }
        else
        {
            Debug.Log("Try again!");
        }
    }
}
