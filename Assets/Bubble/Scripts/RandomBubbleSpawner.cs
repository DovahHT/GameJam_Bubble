using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBubbleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public List<GameObject> bubblePrefabs; // List of bubble prefabs (index = difficulty)
    public float spawnMinX = -5f; // Left boundary for spawning
    public float spawnMaxX = 5f; // Right boundary for spawning
    public float spawnY = -4f; // Fixed Y-coordinate for spawning
    public float minSpawnDelay = 0.5f; // Minimum delay between bubble spawns
    public float maxSpawnDelay = 2f; // Maximum delay between bubble spawns
    public float initialWaveDelay = 2f; // Delay before the first bubble spawns in a wave

    [Header("Wave Settings")]
    public int bubblesPerWaveIncrement = 5; // Number of bubbles added per wave
    private int currentWave = 0; // Current wave number
    private int bubblesToSpawn; // Number of bubbles to spawn in the current wave
    private int unlockedPrefabs = 1; // Number of unlocked bubble prefabs

    private void Start()
    {
        // Initialize the first wave
        bubblesToSpawn = bubblesPerWaveIncrement;
    }

    [ContextMenu("Start Wave")]
    // Call this function to start a wave
    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        HUDScreen.Instance.ShowStartWaveText(currentWave + 1);
        // Wait for the initial delay before starting the wave
        yield return new WaitForSeconds(initialWaveDelay);

        // Spawn bubbles for the current wave
        for (int i = 0; i < bubblesToSpawn; i++)
        {
            SpawnBubble();

            // Wait for a random delay before spawning the next bubble
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);
        }

        // Prepare for the next wave
        currentWave++;
        bubblesToSpawn += bubblesPerWaveIncrement;

        // Unlock the next bubble prefab every 2 waves
        if (currentWave % 2 == 0 && unlockedPrefabs < bubblePrefabs.Count)
        {
            unlockedPrefabs++;
        }
    }

    private void SpawnBubble()
    {
        // Choose a random X position within the spawn boundaries
        float spawnX = Random.Range(spawnMinX, spawnMaxX);
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);

        // Randomly select a bubble prefab based on difficulty progression
        int prefabIndex = GetRandomBubblePrefabIndex();
        GameObject bubblePrefab = bubblePrefabs[prefabIndex];

        // Spawn the bubble
        Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
    }

    private int GetRandomBubblePrefabIndex()
    {
        // Higher-difficulty bubbles have a lower spawn chance initially
        // The chance increases as waves progress
        float[] spawnChances = new float[unlockedPrefabs];
        float totalChance = 0f;

        // Calculate spawn chances for each unlocked prefab
        for (int i = 0; i < unlockedPrefabs; i++)
        {
            spawnChances[i] = Mathf.Pow(0.5f, i); // Decrease chance exponentially
            totalChance += spawnChances[i];
        }

        // Normalize the spawn chances
        for (int i = 0; i < unlockedPrefabs; i++)
        {
            spawnChances[i] /= totalChance;
        }

        // Randomly select a prefab based on the spawn chances
        float randomValue = Random.value;
        float cumulativeChance = 0f;

        for (int i = 0; i < unlockedPrefabs; i++)
        {
            cumulativeChance += spawnChances[i];
            if (randomValue <= cumulativeChance)
            {
                return i;
            }
        }

        // Default to the first prefab (should never happen)
        return 0;
    }
}