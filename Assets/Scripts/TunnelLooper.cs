using UnityEngine;

public class TunnelLooper : MonoBehaviour
{
    public float speed = 5f; // Speed at which the tunnel moves to the left
    public float resetPosition = 10f; // The X position to reset to
    public float offscreenPosition = -10f; // The X position when the tunnel is off-screen

    void Update()
    {
        // Move the tunnel to the left
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Check if the tunnel has moved off-screen
        if (transform.position.x <= offscreenPosition)
        {
            // Reset the tunnel's position to create the looping effect
            Vector2 newPosition = new Vector2(resetPosition, transform.position.y);
            transform.position = newPosition;
        }
    }
}
