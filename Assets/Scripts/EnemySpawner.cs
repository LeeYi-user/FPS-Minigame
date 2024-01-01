using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private PlaySceneManager playSceneManager;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyVariantPrefab;
    [SerializeField] private GameObject bossPrefab;

    public Vector3[] position;

    public static int waves;
    public static int enemySpawnCounter;
    public static int enemyLiveCounter;

    private float nextTimeToSpawn;

    private void Start()
    {
        waves = 1;
        enemySpawnCounter = 0;
        enemyLiveCounter = 0;
    }

    private void Update()
    {
        if (PlaySceneManager.gameOver)
        {
            nextTimeToSpawn = Time.time + 3f;
            return;
        }

        if (enemySpawnCounter == 0 && enemyLiveCounter == 0 && waves % 10 == 0)
        {
            Instantiate(bossPrefab, new Vector3(0f, 15f, 0f), Quaternion.identity);
            nextTimeToSpawn = Time.time + 3f;
            enemyLiveCounter++;
        }

        if (enemySpawnCounter < waves && nextTimeToSpawn < Time.time)
        {
            if (enemySpawnCounter > 0 && enemySpawnCounter % 5 == 0)
            {
                Instantiate(enemyVariantPrefab, position[Random.Range(0, 4)], Quaternion.identity);
            }
            else
            {
                Instantiate(enemyPrefab, position[Random.Range(0, 4)], Quaternion.identity);
            }

            nextTimeToSpawn = Time.time + 1f;
            enemySpawnCounter++;
            enemyLiveCounter++;
        }
        
        if (enemySpawnCounter >= waves && enemyLiveCounter == 0)
        {
            nextTimeToSpawn = Time.time + 3f;
            enemySpawnCounter = 0;
            waves++;
        }
    }
}
