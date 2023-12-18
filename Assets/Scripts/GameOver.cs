using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerCam playerCam;
    public Gun playerGun;
    public Gun playerGun2;

    public GameObject enemyManager;
    public GameObject gameOeverScreen;
    public TextMeshProUGUI gameOverTitle;

    public void Setup(string text)
    {
        GetComponent<SpawnMedkit>().gameover = true;
        GetComponent<SpawnLandmine>().gameover = true;

        playerMovement.gameOver = true;
        playerCam.gameOver = true;
        playerGun.gameOver = true;
        playerGun2.gameOver = true;

        try
        {
            foreach (Enemy enemy in enemyManager.GetComponentsInChildren<Enemy>())
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
