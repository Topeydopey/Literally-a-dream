using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public float spawnInterval = 2f; // Interval between spawns
    public Vector2 spawnRangeY = new Vector2(-4f, 4f); // Vertical range for spawning

    private void Start()
    {
        // Spawn trees at regular intervals
        InvokeRepeating("SpawnTree", 0f, spawnInterval);
    }

    private void SpawnTree()
    {
        // Choose a random Y position within the spawn range
        float spawnY = Random.Range(spawnRangeY.x, spawnRangeY.y);
        Vector3 spawnPosition = new Vector3(transform.position.x, spawnY, 0);

        // Instantiate the tree prefab at the spawn position
        GameObject tree = Instantiate(treePrefab, spawnPosition, Quaternion.identity);
    }
}
