using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private PlaySceneManager playSceneManager;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float slope; // 1
    [SerializeField] private float waveLimit; // 300
    [SerializeField] private float enemyLimit; // 100

    public Vector3[] position;

    public static int waves = 1;
    public static int enemySpawnCounter;
    public static int enemyLiveCounter;

    private float nextTimeToSpawn;

    private void Start()
    {
        enemySpawnCounter = 0;
        enemyLiveCounter = 0;
    }

    private void Update()
    {
        if (PlaySceneManager.gameOver)
        {
            nextTimeToSpawn = Time.time + 2.5f;
            return;
        }

        if (enemySpawnCounter < Sigmoid(waves))
        {
            if (nextTimeToSpawn < Time.time)
            {
                GameObject enemy = Instantiate(enemyPrefab, transform.position + position[Random.Range(0, 4)], Quaternion.identity);
                enemySpawnCounter++;
                enemyLiveCounter++;
                nextTimeToSpawn = Time.time + (10f - Mathf.Sqrt(Sigmoid(waves) / 10f) * 3.5f);
            }
        }
        else if (enemyLiveCounter == 0)
        {
            nextTimeToSpawn = Time.time + 2.5f;
            enemySpawnCounter = 0;
            waves++;
        }
    }

    private int Sigmoid(int w)
    {
        return (int)(enemyLimit / (1f + Mathf.Exp(-slope / (waveLimit / 10f) * (w - (waveLimit / 2f))))) + 1;
    }
}
