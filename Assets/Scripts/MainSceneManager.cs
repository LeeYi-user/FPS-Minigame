using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainSceneManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int score;

    public GameObject medkit;
    public GameObject landmine;

    float medkitTimer;
    float landmineTimer;

    public PlayerMovement playerMovement;
    public PlayerCam playerCam;
    public Gun playerGun;
    public Gun playerGun2;

    public GameObject enemySpawner;
    public GameObject gameOeverScreen;
    public TextMeshProUGUI gameOverTitle;

    private void Start()
    {
        score = 0;
        medkitTimer = 10;
        landmineTimer = 7;
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        medkitTimer -= Time.deltaTime;
        landmineTimer -= Time.deltaTime;

        if (medkitTimer <= 0)
        {
            Instantiate(medkit, new Vector3(Random.Range(-25f, 25f), 15, Random.Range(-25f, 25f)), Quaternion.Euler(-89.98f, 0f, 0f));
            medkitTimer = 10;
        }

        if (landmineTimer <= 0)
        {
            Instantiate(landmine, new Vector3(Random.Range(-25f, 25f), 15, Random.Range(-25f, 25f)), Quaternion.Euler(-89.98f, 0f, 0f));
            landmineTimer = 7;
        }
    }

    public void GameOver(string text)
    {
        playerMovement.gameOver = true;
        playerCam.gameOver = true;
        playerGun.gameOver = true;
        playerGun2.gameOver = true;

        try
        {
            foreach (Enemy enemy in enemySpawner.GetComponentsInChildren<Enemy>())
            {
                enemy.gameOver = true;
            }
        }
        catch
        {
            // pass
        }

        try
        {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>().gameOver = true;
        }
        catch
        {
            // pass
        }

        try
        {
            foreach (GameObject missile in GameObject.FindGameObjectsWithTag("Missile"))
            {
                missile.GetComponent<Missile>().TakeDamage(0);
            }
        }
        catch
        {
            // pass
        }

        gameOeverScreen.SetActive(true);
        gameOverTitle.text = text;
    }

    public void BackGame()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
