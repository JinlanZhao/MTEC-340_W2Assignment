using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    public float Speed = 5.0f;

    public KeyCode LeftDirection = KeyCode.A;
    public KeyCode RightDirection = KeyCode.D;
    public float MinX = -7.5f;
    public float MaxX = 7.5f;

    void Update()
    {
        float movement = 0.0f;

        if (Input.GetKey(LeftDirection))
        {
            movement -= 1.0f;
        }

        if (Input.GetKey(RightDirection))
        {
            movement += 1.0f;
        }

        float newX = transform.position.x + movement * Speed * Time.deltaTime;

        newX = Mathf.Clamp(newX, MinX, MaxX);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
