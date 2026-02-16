using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [Header("Brick Setup")]
    [SerializeField] private GameObject brickPrefab;

    [Header("Grid Settings")]
    [SerializeField] private int rows = 3;
    [SerializeField] private int columns = 10;

    [SerializeField] private float startX = -4.5f;
    [SerializeField] private float startY = 3.5f;

    [SerializeField] private float xSpacing = 1.0f;
    [SerializeField] private float ySpacing = 0.5f;

    private void Start()
    {
        SpawnBricks();
    }

    private void SpawnBricks()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 spawnPosition = new Vector2(
                    startX + col * xSpacing,
                    startY - row * ySpacing
                );

                Instantiate(brickPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
