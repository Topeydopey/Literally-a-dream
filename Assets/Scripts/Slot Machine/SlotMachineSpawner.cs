using UnityEngine;

public class SlotMachineSpawner : MonoBehaviour
{
    public GameObject slotMachinePrefab; // Prefab for the slot machine
    public float spawnInterval = 10f; // Interval between slot machine spawns
    public Vector2 spawnRangeY = new Vector2(-4f, 4f); // Vertical range for spawning
    public int slotMachineSpawnThreshold = 300; // Score required to start spawning slot machines
    public float despawnTime = 10f; // Time before the slot machine despawns

    public GameObject slotMachineUI; // Reference to the inactive Slot Machine UI

    private void Start()
    {
        // Start spawning slot machines at intervals
        InvokeRepeating("TrySpawnSlotMachine", 0f, spawnInterval);
    }

    private void TrySpawnSlotMachine()
    {
        // Get the current score
        int currentScore = ScoreManager.instance != null ? ScoreManager.instance.GetScore() : 0;

        // Spawn the slot machine only if the score threshold is met
        if (currentScore >= slotMachineSpawnThreshold)
        {
            float spawnY = Random.Range(spawnRangeY.x, spawnRangeY.y);
            Vector3 spawnPosition = new Vector3(transform.position.x, spawnY, 0);

            // Instantiate the slot machine prefab
            GameObject slotMachine = Instantiate(slotMachinePrefab, spawnPosition, Quaternion.identity);

            // Attach a trigger script to handle UI activation
            SlotMachineTrigger trigger = slotMachine.AddComponent<SlotMachineTrigger>();
            trigger.slotMachineUI = slotMachineUI;

            // Destroy the slot machine after the despawn time
            Destroy(slotMachine, despawnTime);
        }
    }
}
