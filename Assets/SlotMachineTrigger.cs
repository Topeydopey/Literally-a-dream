using UnityEngine;

public class SlotMachineTrigger : MonoBehaviour
{
    public GameObject slotMachineUI; // Reference to the Slot Machine UI
    public GameObject mainGameUI; // Reference to the Main Game UI

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cat")) // Check if the colliding object is the cat
        {
            // Pause the cat's movement
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.PauseMovement();
            }

            // Activate the Slot Machine UI
            if (slotMachineUI != null)
            {
                // Reset the slot machine state
                SlotMachineSprites slotMachineScript = slotMachineUI.GetComponent<SlotMachineSprites>();
                if (slotMachineScript != null)
                {
                    slotMachineScript.ResetSlotMachine();
                }

                // Show the slot machine UI
                slotMachineUI.SetActive(true);
            }

            // Destroy the slot machine object after interaction
            Destroy(gameObject);
        }
    }
}
