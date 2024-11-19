using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of player-controlled movement
    public float autoMoveSpeed = 2f; // Constant automatic speed to the right
    public Rigidbody2D body;

    [Header("Player 2 Settings")]
    public GameObject player2; // Reference to Player 2 GameObject
    public KeyCode togglePlayer2Key = KeyCode.M; // Key to toggle Player 2

    private void Update()
    {
        HandleMovement();
        HandlePlayer2Toggle();
    }

    private void HandleMovement()
    {
        // Get input for W/D keys (horizontal) and W/S keys (vertical)
        float xInput = 0f;
        float yInput = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            xInput = 1f; // Move right
        }

        if (Input.GetKey(KeyCode.W))
        {
            yInput = 1f; // Move up
        }
        else if (Input.GetKey(KeyCode.S))
        {
            yInput = -1f; // Move down
        }

        // Set automatic rightward movement
        float xVelocity = autoMoveSpeed;

        // Add player-controlled movement on the X-axis
        if (xInput > 0) // Only allow "D" (right input) to increase speed
        {
            xVelocity += xInput * moveSpeed;
        }

        // Prevent backward movement
        xVelocity = Mathf.Max(xVelocity, 0); // Clamp xVelocity to ensure it doesn't go below 0

        // Add vertical movement
        float yVelocity = body.velocity.y; // Keep the current Y velocity if there's no vertical input
        if (Mathf.Abs(yInput) > 0)
        {
            yVelocity = yInput * moveSpeed; // Player-controlled vertical input
        }

        // Set the Rigidbody2D velocity to combine automatic rightward movement and player input
        body.velocity = new Vector2(xVelocity, yVelocity);
    }

    private void HandlePlayer2Toggle()
    {
        // Check if the toggle key is pressed
        if (Input.GetKeyDown(togglePlayer2Key))
        {
            // Toggle Player 2's active state
            if (player2 != null)
            {
                player2.SetActive(!player2.activeSelf);
            }
        }
    }
}
