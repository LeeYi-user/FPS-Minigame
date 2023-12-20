using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    public Slider slider;
    public static float sens = 0.5f;

    private void Start()
    {
        slider.value = sens;
    }

    private void Update()
    {
        sens = slider.value;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
