using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaySceneManager : MonoBehaviour
{
    public static int money;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI wavesText;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject upgradeMenu;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider speedSlider;
    [SerializeField] private Slider jumpSlider;

    [SerializeField] private Slider ammoSlider;
    [SerializeField] private Slider damageSlider;
    [SerializeField] private Slider fireRateSlider;

    public static float health = 60f;
    public static float speed = 4.2f;
    public static float jump = 7.2f;

    public static int ammo = 6;
    public static float damage = 10f;
    public static float fireRate = 2f;

    public static bool gameOver;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$ " + money.ToString();
        wavesText.text = "Waves: " + EnemySpawner.waves.ToString();
        health = healthSlider.value * 300f;
        speed = speedSlider.value * 21f;
        jump = jumpSlider.value * 36f;
        ammo = (int)(ammoSlider.value * 30);
        damage = damageSlider.value * 50f;
        fireRate = fireRateSlider.value * 10f;

        if (Input.GetKeyDown(KeyCode.P))
        {
            healthSlider.interactable = true;
            speedSlider.interactable = true;
            jumpSlider.interactable = true;
            ammoSlider.interactable = true;
            damageSlider.interactable = true;
            fireRateSlider.interactable = true;
        }
    }

    public void Upgrade()
    {
        crosshair.SetActive(false);
        upgradeMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOver = true;
    }

    public void BackFromUpgrade()
    {
        crosshair.SetActive(true);
        Player.maxHealth = health;
        Player.currentHealth = health;
        EnemySpawner.waves = 1;
        EnemySpawner.enemySpawnCounter = 0;
        EnemySpawner.enemyLiveCounter = 0;
        gameOver = false;
    }
}
