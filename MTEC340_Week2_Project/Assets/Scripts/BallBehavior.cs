using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float Speed = 3.0f;
    private Vector2 direction;

    void Start()
    {
        direction = new Vector2(
            Random.value > 0.5f ? 1f : -1f,
            Random.value > 0.5f ? 1f : -1f
        );
        direction.Normalize();
    }

    void Update()
    {
        Vector3 movement = (Vector3)(direction * Speed * Time.deltaTime);
        transform.Translate(movement);

        float radius = 0.0f;
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            radius = sr.bounds.extents.x;
        }

        Camera cam = Camera.main;
        if (cam == null) return;

        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 topRight   = cam.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        float minX = bottomLeft.x + radius;
        float maxX = topRight.x - radius;
        float minY = bottomLeft.y + radius;
        float maxY = topRight.y - radius;

        Vector3 pos = transform.position;

        if (pos.x <= minX)
        {
            pos.x = minX;
            direction.x *= -1f;
        }
        else if (pos.x >= maxX)
        {
            pos.x = maxX;
            direction.x *= -1f;
        }

        if (pos.y <= minY)
        {
            pos.y = minY;
            direction.y *= -1f;
        }
        else if (pos.y >= maxY)
        {
            pos.y = maxY;
            direction.y *= -1f;
        }

        transform.position = pos;
    }
}
