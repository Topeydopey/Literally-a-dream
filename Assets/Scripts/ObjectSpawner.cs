using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    public GameObject humanPrefab;
    public float spawnInterval = 3f; // Interval between human spawns
    public Vector2 spawnRangeY = new Vector2(-4f, 4f); // Vertical range for spawning
    public int humanSpawnThreshold = 50; // Score required to start spawning humans

    private void Start()
    {
        InvokeRepeating("TrySpawnHuman", 0f, spawnInterval);
    }

    private void TrySpawnHuman()
    {
        int currentScore = ScoreManager.instance != null ? ScoreManager.instance.GetScore() : 0;

        if (currentScore >= humanSpawnThreshold)
        {
            float spawnY = Random.Range(spawnRangeY.x, spawnRangeY.y);
            Vector3 spawnPosition = new Vector3(transform.position.x, spawnY, 0);
            Instantiate(humanPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
