using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of player-controlled movement
    public float autoMoveSpeed = 2f; // Constant automatic speed to the right
    public Rigidbody2D body;

    void Update()
    {
        // Get input for horizontal (A/D or Left/Right arrows) and vertical (W/S or Up/Down arrows) movement
        //float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        // Set automatic rightward movement
        float xVelocity = autoMoveSpeed;
        // Add player-controlled movement on the X-axis and Y-axis
        /*
        if (Mathf.Abs(xInput) > 0)
        {
            xVelocity += xInput * moveSpeed; // Player-controlled horizontal input
        }
        */
        float yVelocity = body.velocity.y; // Keep the current Y velocity if there's no vertical input
        if (Mathf.Abs(yInput) > 0)
        {
            yVelocity = yInput * moveSpeed; // Player-controlled vertical input
        }

        // Set the Rigidbody2D velocity to combine automatic rightward movement and player input
        body.velocity = new Vector2(xVelocity, yVelocity);
    }
}
