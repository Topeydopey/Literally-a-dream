using UnityEngine;

public class TunnelLooper : MonoBehaviour
{
    public float speed = 5f; // Speed at which the tunnel moves to the left
    public float resetPosition = 10f; // The X position to reset to
    public float offscreenPosition = -10f; // The X position when the tunnel is off-screen
    private bool stopLooping = false; // Condition to stop looping

    void Update()
    {
        if (stopLooping) return; // Stop moving if stopLooping is true

        // Move the tunnel to the left
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Check if the tunnel has moved off-screen
        if (transform.position.x <= offscreenPosition)
        {
            // If stopLooping is false, reset the tunnel's position
            if (!stopLooping)
            {
                Vector2 newPosition = new Vector2(resetPosition, transform.position.y);
                transform.position = newPosition;
            }
            else
            {
                // Despawn the tunnel by deactivating it
                gameObject.SetActive(false);
            }
        }
    }

    public void StopLooping()
    {
        stopLooping = true;
    }
}
