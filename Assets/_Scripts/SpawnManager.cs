using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    
    private float spawnRange = 9;
    private int _enemyWave = 1;
    private int _enemyCount;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(_enemyWave);
    }

    private void Update()
    {
        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
        _enemyCount = (GameObject.FindObjectsOfType<Enemy>()).Length;

        if (_enemyCount == 0)
        {
            _enemyWave++;
            SpawnEnemyWave(_enemyWave);
            Instantiate(powerUpPrefab, GetRandomSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    /// <summary>
    ///     Generates a Vector 3 randomly, inside the game area.
    /// </summary>
    /// <returns>
    ///     Vector3 inside the game area
    /// </returns>
    private Vector3 GetRandomSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange),
            spawnPosZ = Random.Range(-spawnRange, spawnRange);

        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    /// <summary>
    /// Generates a determined number of enemies
    /// </summary>
    /// <param name="numberOfEnemies">Number of enemies to spawn</param>
    private void SpawnEnemyWave(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemyPrefab, GetRandomSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}
