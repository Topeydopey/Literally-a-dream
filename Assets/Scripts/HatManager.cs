using UnityEngine;

public class HatManager : MonoBehaviour
{
    public Transform catHead; // Reference to the cat's head position
    public float dropSpeed = 5f; // Speed of the hat dropping
    public float riseSpeed = 5f; // Speed of the hat rising
    public Vector3 offset = new Vector3(0, 1, 0); // Offset from the cat's head when rising

    private Vector3 targetPosition; // Target position for the hat
    private bool isNight = false; // Tracks if it's night

    private void Start()
    {
        // Start with the hat above the cat's head
        transform.position = catHead.position + offset;
    }

    private void Update()
    {
        // Smoothly move the hat toward the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, (isNight ? dropSpeed : riseSpeed) * Time.deltaTime);
    }

    public void SetNight(bool night)
    {
        isNight = night;

        // Set the target position for the hat
        if (isNight)
        {
            // Drop onto the cat's head
            targetPosition = catHead.position;
        }
        else
        {
            // Rise back above the cat's head
            targetPosition = catHead.position + offset;
        }
    }
}
