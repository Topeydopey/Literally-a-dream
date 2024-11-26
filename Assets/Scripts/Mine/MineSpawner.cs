using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    public GameObject minePrefab; // Prefab for the mine
    public float spawnInterval = 5f; // Interval between mine spawns
    public Vector2 spawnRangeY = new Vector2(-4f, 4f); // Vertical range for spawning
    public int mineSpawnThreshold = 100; // Score required to start spawning mines
    public float despawnTime = 10f; // Time before the mine despawns

    private void Start()
    {
        // Start spawning mines at regular intervals
        InvokeRepeating("TrySpawnMine", 0f, spawnInterval);
    }

    private void TrySpawnMine()
    {
        // Get the current score
        int currentScore = ScoreManager.instance != null ? ScoreManager.instance.GetScore() : 0;

        // Only spawn mines if the score threshold is met
        if (currentScore >= mineSpawnThreshold)
        {
            // Choose a random Y position within the spawn range
            float spawnY = Random.Range(spawnRangeY.x, spawnRangeY.y);
            Vector3 spawnPosition = new Vector3(transform.position.x, spawnY, 0);

            // Instantiate the mine prefab and schedule its despawn
            GameObject mine = Instantiate(minePrefab, spawnPosition, Quaternion.identity);
            Destroy(mine, despawnTime); // Destroy the mine after the despawn time
        }
    }
}
