using UnityEngine;
using TMPro;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;

    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private GameObject _brickPrefab;
    public static GameState CurrentState = GameState.Playing;

    [Header("Score UI")]
    [SerializeField] private TMP_Text _scoreLeftText;
    [SerializeField] private TMP_Text _scoreMiddleText;
    [SerializeField] private TMP_Text _scoreRightText;

    private int _scoreLeft;
    private int _scoreMiddle;
    private int _scoreRight;

    public int ScoreLeft
    {
        get => _scoreLeft;
        set
        {
            _scoreLeft = value;
            if (_scoreLeftText != null)
                _scoreLeftText.text = _scoreLeft.ToString();
        }
    }

    public int ScoreMiddle
    {
        get => _scoreMiddle;
        set
        {
            _scoreMiddle = value;
            if (_scoreMiddleText != null)
                _scoreMiddleText.text = _scoreMiddle.ToString();
        }
    }

    public int ScoreRight
    {
        get => _scoreRight;
        set
        {
            _scoreRight = value;
            if (_scoreRightText != null)
                _scoreRightText.text = _scoreRight.ToString();
        }
    }

    // Track the one authoritative ball in the scene
    private GameObject _activeBall;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // destroy the whole duplicate object
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ScoreLeft = 0;
        ScoreMiddle = 0;
        ScoreRight = 0; 
        Serve();
    }

    private void Serve()
    {
        // Don't spawn if a ball already exists
        if (_activeBall != null) return;

        _activeBall = Instantiate(_ballPrefab, Vector3.zero, Quaternion.identity);
    }

    // Call this when the ball falls into the MissZone
    public void HandleMiss()
    {
        // Prevent multiple pending respawns
        CancelInvoke(nameof(Serve));

        if (_activeBall != null)
        {
            Destroy(_activeBall);
            _activeBall = null;
        }

        Invoke(nameof(Serve), 2.0f);
    }

    public enum GameState
    {
        Playing,
        Paused
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (CurrentState == GameState.Playing)
                PauseGame();
        else
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        CurrentState = GameState.Paused;

        // Pause Ball physics
        if (_activeBall != null)
        {
            var rb = _activeBall.GetComponent<Rigidbody2D>();
            if (rb != null) rb.simulated = false;
        }
    }

    private void ResumeGame()
    {
        CurrentState = GameState.Playing;

        // Resume Ball physics
        if (_activeBall != null)
        {
            var rb = _activeBall.GetComponent<Rigidbody2D>();
            if (rb != null) rb.simulated = true;
        }
    }
}
