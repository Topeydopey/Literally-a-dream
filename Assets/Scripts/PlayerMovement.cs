using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    public Rigidbody2D body;

    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(xInput) > 0)
        {
            body.velocity = new Vector2(xInput * MoveSpeed, body.velocity.y);
        }
        if (Mathf.Abs(yInput) > 0)
        {
            body.velocity = new Vector2(body.velocity.x, yInput * MoveSpeed);
        }
    }
}