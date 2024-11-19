using UnityEngine;

public class ArrowKeyPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of player-controlled movement
    public float autoMoveSpeed = 2f; // Constant automatic speed to the right
    public Rigidbody2D body;

    void Update()
    {
        // Get input for horizontal (Left/Right arrows) and vertical (Up/Down arrows) movement
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        // Set automatic rightward movement
        float xVelocity = autoMoveSpeed;

        // Add player-controlled movement on the X-axis
        if (xInput > 0) // Right arrow key increases speed
        {
            xVelocity += xInput * moveSpeed;
        }
        else if (xInput < 0) // Left arrow key decreases speed
        {
            xVelocity += xInput * moveSpeed; // Reduces velocity, allowing backward movement
        }

        // Add vertical movement
        float yVelocity = body.velocity.y; // Keep the current Y velocity if there's no vertical input
        if (Mathf.Abs(yInput) > 0)
        {
            yVelocity = yInput * moveSpeed; // Player-controlled vertical input
        }

        // Set the Rigidbody2D velocity to combine automatic rightward movement and player input
        body.velocity = new Vector2(xVelocity, yVelocity);
    }
}
