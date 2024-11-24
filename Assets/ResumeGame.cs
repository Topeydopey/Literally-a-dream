using UnityEngine;

public class ResumeGame : MonoBehaviour
{
    public GameObject slotMachineUI; // Reference to the Slot Machine UI
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
    }
}
