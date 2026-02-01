using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private BoxCollider2D spawnArea;
    [SerializeField] private int gridCount;
    [SerializeField] private int enemyGridDensity;
    [SerializeField] private GameObject spawnParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemies();
    }
    private void SpawnEnemies()
    {
        float chunkX = spawnArea.size.x / gridCount;
        float chunkY = spawnArea.size.y / gridCount;
        for (int i = 0; i < gridCount; i++)
        {
            for(int j = 0; j < gridCount; j++)
            {
                for(int k = 0; k < enemyGridDensity; k++)
                {
                    Vector2 spawnPosition = new Vector2(
                        Random.Range(spawnArea.bounds.min.x + i * chunkX, spawnArea.bounds.min.x + (i + 1) * chunkX),
                        Random.Range(spawnArea.bounds.min.y + j * chunkY, spawnArea.bounds.min.y + (j + 1) * chunkY)
                    );
                    Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, spawnParent.transform);
                }
            }
        }
    }
}
