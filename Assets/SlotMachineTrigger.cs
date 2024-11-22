using UnityEngine;

public class SlotMachineTrigger : MonoBehaviour
{
    public GameObject slotMachineUI; // Reference to the Slot Machine UI (Canvas or parent UI object)
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
                slotMachineUI.SetActive(true); // Enable the UI first

                // Reset the slot machine state after activation
                SlotMachineSprites slotMachineScript = slotMachineUI.GetComponent<SlotMachineSprites>();
                if (slotMachineScript != null)
                {
                    slotMachineScript.ResetSlotMachine();
                }
                else
                {
                    Debug.LogError("SlotMachineSprites script not found on SlotMachineUI.");
                }
            }

            // Destroy the slot machine object after interaction
            Destroy(gameObject);
        }
    }
}
