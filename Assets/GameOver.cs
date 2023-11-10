using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Camera playerCam;
    public GameObject gun;
    public GameObject gun2;
    public GameObject enemies;
    public GameObject player;
    public GameObject arena;

    public void Setup(string text)
    {
        playerCam.GetComponent<PlayerCam>().gameOver = true;
        gun.GetComponent<Gun>().gameOver = true;
        gun2.GetComponent<Gun>().gameOver = true;
        arena.GetComponent<SpawnMedkit>().gameover = true;

        try
        {
            foreach (Enemy enemy in enemies.GetComponentsInChildren<Enemy>())
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
            foreach (GameObject missile in GameObject.FindGameObjectsWithTag("Missile"))
            {
                missile.GetComponent<Missile>().TakeDamage(0);
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

        player.GetComponent<PlayerMovement>().gameOver = true;
        gameObject.SetActive(true);
        title.text = text;
    }

    public void BackGame()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
