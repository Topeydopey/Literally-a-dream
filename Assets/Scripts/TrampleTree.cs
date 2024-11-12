using UnityEngine;

public class TrampleTree : MonoBehaviour
{
    public GameObject trampledVersion; // A version of the tree that appears after trampling
    public float despawnDelay = 5f; // Delay before this tree despawns
    public int scoreValue = 10;

    private void Start()
    {
        // Destroy the tree after the specified despawn delay
        Destroy(gameObject, despawnDelay);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cat"))
        {
            // Trample the tree when the cat collides with it
            Trample();
        }
    }

    private void Trample()
    {
        if (trampledVersion != null)
        {
            // Instantiate the trampled version of the tree and destroy the original
            Instantiate(trampledVersion, transform.position, transform.rotation);
        }

        // Add score when the tree is trampled
        ScoreManager.instance.AddScore(scoreValue);
        Destroy(gameObject);

    }
}
