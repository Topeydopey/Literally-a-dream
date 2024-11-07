using UnityEngine;

public class WindowViewMovement : MonoBehaviour
{
    public float moveAmount = 0.1f;   // Maximum amount to move
    public float moveSpeed = 1f;      // Speed of movement

    private Vector3 initialPosition;  // Original position of the sprite

    void Start()
    {
        // Store the starting position
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Create a subtle movement by oscillating around the initial position
        float offsetX = Mathf.PerlinNoise(Time.time * moveSpeed, 0) - 0.5f;
        float offsetY = Mathf.PerlinNoise(0, Time.time * moveSpeed) - 0.5f;

        // Apply the offset to the initial position
        transform.localPosition = initialPosition + new Vector3(offsetX, offsetY, 0) * moveAmount;
    }
}
