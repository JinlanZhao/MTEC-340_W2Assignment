using System.Security.Cryptography;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [SerializeField] private float _launchForce = 3.0f;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        ResetBall();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        ResetBall();
    }
    void ResetBall()
    {
        _rb.linearVelocity = Vector2.zero;
        transform.position = Vector3.zero;

        Vector2 direction = new Vector2(
        direction.y = GetNonZeroRandomFloat(),
        direction.x = GetNonZeroRandomFloat()
        ).normalized;  // Normalized ensure the magnitude of our vector is 1.

        _rb.AddForce(direction * _launchForce, ForceMode2D.Impulse);

    }
        float GetNonZeroRandomFloat(float min = -1.0f, float max = 1.0f)
    {
        float num;

        do
        {
            num = Random.Range(min, max);
        } while (Mathf.Approximately(num, 0.0f)); //avoid num == 0.0f (people usually don't equal something to a float. )

        return num;
    }
}