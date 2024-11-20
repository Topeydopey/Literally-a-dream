using UnityEngine;

public class TrampleObject : MonoBehaviour
{
    public GameObject trampledVersion; // A version of the object that appears after being trampled
    public int scoreValue = 10;
    public string trampleTag = "Cat"; // Tag of the object that can trample this
    public AudioClip[] trampleSounds; // Array of sounds to play when trampled
    public float soundVolume = 1f; // Volume of the trample sound (0 to 1)

    private bool isTrampled = false; // Ensure trample logic runs only once

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(trampleTag) && !isTrampled)
        {
            isTrampled = true; // Mark as trampled to prevent duplicate execution
            Trample();
        }
    }

    private void Trample()
    {
        // Choose a random sound to play
        AudioClip randomSound = null;
        if (trampleSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, trampleSounds.Length);
            randomSound = trampleSounds[randomIndex];
        }

        // Instantiate the trampled version
        if (trampledVersion != null)
        {
            GameObject trampledObject = Instantiate(trampledVersion, transform.position, transform.rotation);

            // Pass the sound and volume to the TrampledSoundPlayer
            TrampledSoundPlayer soundPlayer = trampledObject.GetComponent<TrampledSoundPlayer>();
            if (soundPlayer != null && randomSound != null)
            {
                soundPlayer.PlaySound(randomSound, soundVolume); // Pass volume here
            }
        }

        // Add score when the object is trampled
        ScoreManager.instance.AddScore(scoreValue);

        // Destroy the original object
        Destroy(gameObject);
    }
}
