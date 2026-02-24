using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [SerializeField] private float _launchForce = 3.0f;
    [SerializeField] private float _brickInfluence = 0.3f;
    [SerializeField] private float _speedMultiplier = 1.1f;
    private Rigidbody2D _rb;
    private Vector3 _spawnPos;

    private AudioSource _source;
    [SerializeField] private AudioClip _paddleHit;
    [SerializeField] private AudioClip _brickHit;
    [SerializeField] private AudioClip _MissScore;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _source = GetComponent<AudioSource>();

        Vector2 direction = Random.insideUnitCircle;

        // Abs calculate the absolute value and compares it against a threshold. 
        if (Mathf.Abs(direction.x) < 0.25f)
        {
            // Sign returns 1 or minus 1 depending on the whether the input is positive or negative.
            direction.x += 0.5f * Mathf.Sign(direction.x);
        }

        _spawnPos = transform.position;
        _rb.AddForce(direction * _launchForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Brick"))
        {
            // This will execute when the paddle is moving
            if (!Mathf.Approximately(other.rigidbody.linearVelocity.y, 0.0f))
            {
                // Weighted Sum and one-minus
                Vector2 direction = _rb.linearVelocity * (1.0f - _brickInfluence)
                                    + other.rigidbody.linearVelocity * _brickInfluence;

                // Magnitude is the length of a vector, and we use it to maintain the same speed.
                // Normalization allows the length of the direction to always be 1.
                _rb.linearVelocity = _rb.linearVelocity.magnitude * direction.normalized;
            }

            _rb.linearVelocity *= _speedMultiplier;
            _source.resource = _brickHit;
        }
        else
        {
            _source.resource = _paddleHit;
        }

        _source.Play();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (GameBehavior.CurrentState == GameBehavior.GameState.Paused)
            return;

        if (!other.CompareTag("Miss"))
            return;

        _source.PlayOneShot(_MissScore);
        Invoke(nameof(DoMiss), 0.2f);
    }

    private void DoMiss()
    {
        GameBehavior.Instance.HandleMiss();
    }

    private void FixedUpdate()
    {
        PreventHorizontalLock();
    }

    private void PreventHorizontalLock()
    {
        Vector2 velocity = _rb.linearVelocity;

        if (Mathf.Abs(velocity.y) < 0.5f)
        {
            velocity.y = 0.5f * Mathf.Sign(velocity.y == 0 ? 1 : velocity.y);
            _rb.linearVelocity = velocity.normalized * _rb.linearVelocity.magnitude;
        }
    }
}