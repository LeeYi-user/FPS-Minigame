using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private PlaySceneManager playSceneManager;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyVariantPrefab;
    [SerializeField] private GameObject bossPrefab;

    [SerializeField] private float slope;
    [SerializeField] private float waveLimit;
    [SerializeField] private float enemyLimit;

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
            nextTimeToSpawn = Time.time + 2f;
            return;
        }

        if (enemySpawnCounter == 0 && enemyLiveCounter == 0 && waves % 10 == 0)
        {
            Instantiate(bossPrefab, new Vector3(0f, 15f, 0f), Quaternion.identity);
            nextTimeToSpawn = Time.time + 2f;
            enemyLiveCounter++;
        }

        if (enemySpawnCounter < Sigmoid(waves) && nextTimeToSpawn < Time.time)
        {
            if (waves % 3 == 0 && enemySpawnCounter > 0 && enemySpawnCounter % 3 == 0)
            {
                Instantiate(enemyVariantPrefab, position[Random.Range(0, 4)], Quaternion.identity);
            }
            else
            {
                Instantiate(enemyPrefab, position[Random.Range(0, 4)], Quaternion.identity);
            }

            nextTimeToSpawn = Time.time + 1.5f;
            enemySpawnCounter++;
            enemyLiveCounter++;
        }
        
        if (enemySpawnCounter >= Sigmoid(waves) && enemyLiveCounter == 0)
        {
            nextTimeToSpawn = Time.time + 2f;
            enemySpawnCounter = 0;
            waves++;
        }
    }

    private int Sigmoid(int w)
    {
        return (int)(enemyLimit / (1f + Mathf.Exp(-slope / (waveLimit / 10f) * (w - (waveLimit / 2f))))) + 1;
    }
}
