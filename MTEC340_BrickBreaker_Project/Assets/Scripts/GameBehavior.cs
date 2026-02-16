using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;

    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private GameObject _brickPrefab;

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

        Invoke(nameof(Serve), 1.0f);
    }

    public void Score()
    {
        // add score logic/UI later
    }
}
