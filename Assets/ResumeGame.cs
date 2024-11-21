using UnityEngine;

public class ResumeGame : MonoBehaviour
{
    public GameObject slotMachineUI; // Reference to the Slot Machine UI
    public GameObject mainGameUI; // Reference to the Main Game UI
    public GameObject player; // Reference to the cat player

    public void Resume()
    {
        // Resume the cat's movement
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.ResumeMovement();
        }

        // Hide the Slot Machine UI
        if (slotMachineUI != null)
        {
            slotMachineUI.SetActive(false);
        }

        // Re-enable the Main Game UI
        if (mainGameUI != null)
        {
            mainGameUI.SetActive(true);
        }
    }
}
