using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float backgroundWidth = 10f; // Width of a single background sprite

    private Transform[] backgrounds;

    void Start()
    {
        // Get all child transforms as background pieces
        backgrounds = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
        // Loop through all background segments
        foreach (Transform background in backgrounds)
        {
            // Check if the background is behind the player
            if (background.position.x + backgroundWidth / 2 < player.position.x)
            {
                // Reposition the background to the far right
                float rightmostX = GetRightmostBackgroundX();
                background.position = new Vector3(rightmostX + backgroundWidth, background.position.y, background.position.z);
            }
        }
    }

    private float GetRightmostBackgroundX()
    {
        float rightmostX = float.MinValue;
        foreach (Transform background in backgrounds)
        {
            if (background.position.x > rightmostX)
            {
                rightmostX = background.position.x;
            }
        }
        return rightmostX;
    }
}
