using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    public GameObject humanPrefab; // Prefab for human
    public float spawnInterval = 3f; // Interval between human spawns
    public Vector2 spawnRangeY = new Vector2(-4f, 4f); // Vertical range for spawning
    public int humanSpawnThreshold = 50; // Score required to start spawning humans
    public float despawnTime = 5f; // Time before the human despawns

    private void Start()
    {
        InvokeRepeating("TrySpawnHuman", 0f, spawnInterval);
    }

    private void TrySpawnHuman()
    {
        // Get the current score
        int currentScore = ScoreManager.instance != null ? ScoreManager.instance.GetScore() : 0;

        // Only spawn humans if the score threshold is met
        if (currentScore >= humanSpawnThreshold)
        {
            float spawnY = Random.Range(spawnRangeY.x, spawnRangeY.y);
            Vector3 spawnPosition = new Vector3(transform.position.x, spawnY, 0);

            // Instantiate the human prefab and schedule its despawn
            GameObject human = Instantiate(humanPrefab, spawnPosition, Quaternion.identity);
            Destroy(human, despawnTime); // Destroy the human after the despawn time
        }
    }
}
