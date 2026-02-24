using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    [SerializeField] private string ballTag = "Ball";
    [SerializeField] private int _lives = 3;
    private SpriteRenderer _spriteRenderer;
    private readonly Color[] _lifeColors = {Color.red, Color.yellow, Color.green};
    public int Lives
    {
        get => _lives;
        set
        {
            _lives = value;
            UpdateColor();
        }
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    private void UpdateColor()
    {
        int index = Mathf.Clamp(_lives, 1, _lifeColors.Length) -1;

        if(_spriteRenderer != null)
        _spriteRenderer.color = _lifeColors[index];
    }

    // If Brick uses Collider2D (Is Trigger = OFF) and Ball has Rigidbody2D:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameBehavior.CurrentState == GameBehavior.GameState.Paused)
            return;
        if (!collision.gameObject.CompareTag(ballTag))
            return;

        Debug.Log($"Hit {name}, Lives before = {Lives}");
        Lives -= 1;
        if(Lives <= 0)
        {
            GameBehavior.Instance.ScoreMiddle += 1;
            Destroy(gameObject);
        }
    }
}
