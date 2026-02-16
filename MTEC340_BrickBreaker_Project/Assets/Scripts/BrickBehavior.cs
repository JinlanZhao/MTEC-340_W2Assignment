using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    [SerializeField] private string ballTag = "Ball";

    // If Brick uses Collider2D (Is Trigger = OFF) and Ball has Rigidbody2D:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ballTag))
        {
            Destroy(gameObject);
        }
    }

    // If Brick uses Collider2D (Is Trigger = ON):
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ballTag))
        {
            Destroy(gameObject);
        }
    }
}
