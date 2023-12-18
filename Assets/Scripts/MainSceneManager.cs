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

    public GameObject enemySpawner;
    public GameObject gameOeverScreen;
    public TextMeshProUGUI gameOverTitle;

    public static bool gameover;

    private void Start()
    {
        score = 0;
        medkitTimer = 10;
        landmineTimer = 7;
        gameover = false;
    }

    private void Update()
    {
        if (gameover)
        {
            return;
        }

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
        gameover = true;
        gameOverTitle.text = text;
        gameOeverScreen.SetActive(true);

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

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void BackGame()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
