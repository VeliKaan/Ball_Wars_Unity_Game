using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Reference to the GameManager that controls the game state
    public GameManager GameManager;

    // Prefabs for the enemy, boss, and power-up
    public GameObject[] EnemyPrefabs;
    public GameObject BossPrefab;
    public GameObject PowerupPrefab;

    // Interval for spawning the boss (every 3 waves)
    private static readonly int BossSpawnInterval = 3;

    // Tracks the current wave number
    private int currentWave = 0;

    // Update is called once per frame
    private void Update()
    {
        // Check if all enemies are defeated and the game is not over
        if (IsWaveCleared() && !GameManager.GameOver)
        {
            SpawnPowerup(); // Spawn a power-up after each cleared wave
            SpawnNextWave(); // Spawn the next wave of enemies
        }
    }

    // Returns true if no enemies are present in the scene
    private bool IsWaveCleared()
    {
        return FindObjectsOfType<Enemy>().Length == 0;
    }

    // Determines the number of enemies in the next wave and spawns them
    private void SpawnNextWave()
    {
        currentWave++;

        // Check if the current wave is a boss wave (every 3 waves)
        bool isBossWave = (currentWave - 1) % (1 + BossSpawnInterval) == BossSpawnInterval;
        int regularEnemiesCount = isBossWave ? currentWave - 1 : currentWave;

        // Spawn boss if it's a boss wave
        if (isBossWave)
        {
            SpawnBoss();
        }

        // Spawn regular enemies
        SpawnEnemies(regularEnemiesCount);
    }

    // Generates a random spawn position
    private Vector3 GenerateSpawnPosition()
    {
        return SpawnPosition.Generate(); // Calls the Generate method from SpawnPosition to get random spawn points
    }

    // Spawns a specified number of regular enemies
    private void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Select a random enemy prefab and spawn it
            var enemyPrefab = EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)];
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    // Spawns the boss enemy
    private void SpawnBoss()
    {
        Instantiate(BossPrefab, GenerateSpawnPosition(), BossPrefab.transform.rotation);
    }

    // Spawns a random power-up
    private void SpawnPowerup()
    {
        var powerType = GetRandomPowerType(); // Get a random power-up type
        var powerupInstance = Instantiate(PowerupPrefab, GenerateSpawnPosition(), PowerupPrefab.transform.rotation);
        powerupInstance.GetComponent<Powerup>().PowerType = powerType; // Assign the random power type to the power-up
    }

    // Selects a random power-up type from the PowerType enum
    private PowerType GetRandomPowerType()
    {
        var powerTypes = System.Enum.GetValues(typeof(PowerType));
        return (PowerType)powerTypes.GetValue(Random.Range(0, powerTypes.Length)); // Randomly select a power-up type
    }
}
