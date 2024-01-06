using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlaySceneManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI wavesText;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject upgradeMenu;

    public static int money;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider speedSlider;
    [SerializeField] private Slider jumpSlider;

    [SerializeField] private Slider ammoSlider;
    [SerializeField] private Slider damageSlider;
    [SerializeField] private Slider fireRateSlider;

    private static float healthSliderValue = 2f;
    private static float speedSliderValue = 2f;
    private static float jumpSliderValue = 2f;

    private static float ammoSliderValue = 2f;
    private static float damageSliderValue = 2f;
    private static float fireRateSliderValue = 2f;

    private static int healthPrice = 100;
    private static int speedPrice = 100;
    private static int jumpPrice = 100;

    private static int ammoPrice = 100;
    private static int damagePrice = 100;
    private static int fireRatePrice = 100;

    [SerializeField] private TextMeshProUGUI healthPriceText;
    [SerializeField] private TextMeshProUGUI speedPriceText;
    [SerializeField] private TextMeshProUGUI jumpPriceText;

    [SerializeField] private TextMeshProUGUI ammoPriceText;
    [SerializeField] private TextMeshProUGUI damagePriceText;
    [SerializeField] private TextMeshProUGUI fireRatePriceText;

    public static float health = 60f;
    public static float speed = 5.6f;
    public static float jump = 10.4f;

    public static int ammo = 6;
    public static float damage = 10f;
    public static float fireRate = 2f;

    [SerializeField] private MeshRenderer[] weaponSkins;
    [SerializeField] private WeaponSwitch weaponSwitch;

    public static bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameOver = false;
        UpdateSlider();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$ " + money.ToString();
        wavesText.text = "Waves: " + EnemySpawner.waves.ToString();

        if (Input.GetKeyDown(KeyCode.P))
        {
            money += 100000;
        }
    }

    public void Upgrade()
    {
        foreach (MeshRenderer skin in weaponSkins)
        {
            skin.enabled = false;
        }

        crosshair.SetActive(false);
        upgradeMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOver = true;
    }

    public void UpgradeHealth()
    {
        if (healthSliderValue >= 10f)
        {
            return;
        }

        if (money >= healthPrice)
        {
            healthSliderValue += 1f;
            health = healthSliderValue * 30f;
            money -= healthPrice;
            healthPrice *= 2;
            UpdateSlider();
        }
    }

    public void UpgradeSpeed()
    {
        if (speedSliderValue >= 10f)
        {
            return;
        }

        if (money >= speedPrice)
        {
            speedSliderValue += 1f;
            speed = speedSliderValue * 2.8f;
            money -= speedPrice;
            speedPrice *= 2;
            UpdateSlider();
        }
    }

    public void UpgradeJump()
    {
        if (jumpSliderValue >= 10f)
        {
            return;
        }

        if (money >= jumpPrice)
        {
            jumpSliderValue += 1f;
            jump = jumpSliderValue * 3.2f + 4;
            money -= jumpPrice;
            jumpPrice *= 2;
            UpdateSlider();
        }
    }

    public void UpgradeAmmo()
    {
        if (ammoSliderValue >= 10f)
        {
            return;
        }

        if (money >= ammoPrice)
        {
            ammoSliderValue += 1f;
            ammo = (int)(ammoSliderValue * 3);
            money -= ammoPrice;
            ammoPrice *= 2;
            UpdateSlider();
        }
    }

    public void UpgradeDamage()
    {
        if (damageSliderValue >= 10f)
        {
            return;
        }

        if (money >= damagePrice)
        {
            damageSliderValue += 1f;
            damage = damageSliderValue * 5f;
            money -= damagePrice;
            damagePrice *= 2;
            UpdateSlider();
        }
    }

    public void UpgradeFireRate()
    {
        if (fireRateSliderValue >= 10f)
        {
            return;
        }

        if (money >= fireRatePrice)
        {
            fireRateSliderValue += 1f;
            fireRate = fireRateSliderValue;
            money -= fireRatePrice;
            fireRatePrice *= 2;
            UpdateSlider();
        }
    }

    private void UpdateSlider()
    {
        healthSlider.value = healthSliderValue;
        speedSlider.value = speedSliderValue;
        jumpSlider.value = jumpSliderValue;
        ammoSlider.value = ammoSliderValue;
        damageSlider.value = damageSliderValue;
        fireRateSlider.value = fireRateSliderValue;

        if (healthSlider.value < 10f)
        {
            healthPriceText.text = healthPrice.ToString() + " $";
        }
        else
        {
            healthPriceText.text = "MAX";
        }

        if (speedSlider.value < 10f)
        {
            speedPriceText.text = speedPrice.ToString() + " $";
        }
        else
        {
            speedPriceText.text = "MAX";
        }

        if (jumpSlider.value < 10f)
        {
            jumpPriceText.text = jumpPrice.ToString() + " $";
        }
        else
        {
            jumpPriceText.text = "MAX";
        }

        if (ammoSlider.value < 10f)
        {
            ammoPriceText.text = ammoPrice.ToString() + " $";
        }
        else
        {
            ammoPriceText.text = "MAX";
        }

        if (damageSlider.value < 10f)
        {
            damagePriceText.text = damagePrice.ToString() + " $";
        }
        else
        {
            damagePriceText.text = "MAX";
        }

        if (fireRateSlider.value < 10f)
        {
            fireRatePriceText.text = fireRatePrice.ToString() + " $";
        }
        else
        {
            weaponSwitch.SelectWeapon(1);
            fireRatePriceText.text = "MAX";
        }
    }

    public void BackFromUpgrade()
    {
        foreach (MeshRenderer skin in weaponSkins)
        {
            skin.enabled = true;
        }

        player.transform.position = new Vector3(0f, 1f, 0f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        crosshair.SetActive(true);
        Player.maxHealth = health;
        Player.currentHealth = health;
        EnemySpawner.waves = 1;
        EnemySpawner.bossWave = 8;
        EnemySpawner.enemySpawnCounter = 0;
        EnemySpawner.enemyLiveCounter = 0;
        gameOver = false;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
