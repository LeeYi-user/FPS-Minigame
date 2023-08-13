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
    public GameObject enemy;
    public GameObject player;

    public void Setup(string text)
    {
        playerCam.GetComponent<PlayerCam>().gameOver = true;
        gun.GetComponent<Gun>().gameOver = true;
        enemy.GetComponent<Enemy>().gameOver = true;
        player.GetComponent<PlayerMovement>().gameOver = true;
        gameObject.SetActive(true);
        title.text = text;
    }

    public void BackGame()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
