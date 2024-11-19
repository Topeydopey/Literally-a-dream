using UnityEngine;

public class TrampleObject : MonoBehaviour
{
    public GameObject trampledVersion; // A version of the object that appears after being trampled
    public float despawnDelay = 5f; // Delay before this object despawns
    public int scoreValue = 10;
    public string trampleTag = "Cat"; // Tag of the object that can trample this
    public AudioClip[] trampleSounds; // Array of sounds to play when trampled

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(trampleTag))
        {
            Trample();
        }
    }

    private void Trample()
    {
        if (trampledVersion != null)
        {
            // Choose a random sound to play
            AudioClip randomSound = null;
            if (trampleSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, trampleSounds.Length);
                randomSound = trampleSounds[randomIndex];
            }

            // Instantiate the trampled version and pass the sound
            GameObject trampledObject = Instantiate(trampledVersion, transform.position, transform.rotation);
            TrampledSoundPlayer soundPlayer = trampledObject.GetComponent<TrampledSoundPlayer>();
            if (soundPlayer != null && randomSound != null)
            {
                soundPlayer.PlaySound(randomSound);
            }
        }

        // Add score when the object is trampled
        ScoreManager.instance.AddScore(scoreValue);

        // Destroy the original object
        Destroy(gameObject);
    }
}
