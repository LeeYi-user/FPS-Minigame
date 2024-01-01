using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public static int healthPrice = 100;
    public static int speedPrice = 100;
    public static int jumpPrice = 100;

    public static int ammoPrice = 100;
    public static int damagePrice = 100;
    public static int fireRatePrice = 100;

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

    [SerializeField] private WeaponSwitch weaponSwitch;

    public static bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        crosshair.SetActive(false);
        upgradeMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOver = true;
    }

    public void UpgradeHealth()
    {
        if (healthSlider.value >= 10f)
        {
            return;
        }

        if (money >= healthPrice)
        {
            healthSlider.value += 1f;
            health = healthSlider.value * 30f;
            money -= healthPrice;
            healthPrice *= 2;

            if (healthSlider.value < 10f)
            {
                healthPriceText.text = healthPrice.ToString() + " $";
            }
            else
            {
                healthPriceText.text = "MAX";
            }
        }
    }

    public void UpgradeSpeed()
    {
        if (speedSlider.value >= 10f)
        {
            return;
        }

        if (money >= speedPrice)
        {
            speedSlider.value += 1f;
            speed = speedSlider.value * 2.8f;
            money -= speedPrice;
            speedPrice *= 2;

            if (speedSlider.value < 10f)
            {
                speedPriceText.text = speedPrice.ToString() + " $";
            }
            else
            {
                speedPriceText.text = "MAX";
            }
        }
    }

    public void UpgradeJump()
    {
        if (jumpSlider.value >= 10f)
        {
            return;
        }

        if (money >= jumpPrice)
        {
            jumpSlider.value += 1f;
            jump = jumpSlider.value * 3.2f + 4;
            money -= jumpPrice;
            jumpPrice *= 2;

            if (jumpSlider.value < 10f)
            {
                jumpPriceText.text = jumpPrice.ToString() + " $";
            }
            else
            {
                jumpPriceText.text = "MAX";
            }
        }
    }

    public void UpgradeAmmo()
    {
        if (ammoSlider.value >= 10f)
        {
            return;
        }

        if (money >= ammoPrice)
        {
            ammoSlider.value += 1f;
            ammo = (int)(ammoSlider.value * 3);
            money -= ammoPrice;
            ammoPrice *= 2;

            if (ammoSlider.value < 10f)
            {
                ammoPriceText.text = ammoPrice.ToString() + " $";
            }
            else
            {
                ammoPriceText.text = "MAX";
            }
        }
    }

    public void UpgradeDamage()
    {
        if (damageSlider.value >= 10f)
        {
            return;
        }

        if (money >= damagePrice)
        {
            damageSlider.value += 1f;
            damage = damageSlider.value * 5f;
            money -= damagePrice;
            damagePrice *= 2;

            if (damageSlider.value < 10f)
            {
                damagePriceText.text = damagePrice.ToString() + " $";
            }
            else
            {
                damagePriceText.text = "MAX";
            }
        }
    }

    public void UpgradeFireRate()
    {
        if (fireRateSlider.value >= 10f)
        {
            return;
        }

        if (money >= fireRatePrice)
        {
            fireRateSlider.value += 1f;
            fireRate = fireRateSlider.value;
            money -= fireRatePrice;
            fireRatePrice *= 2;

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
    }

    public void BackFromUpgrade()
    {
        player.transform.position = new Vector3(0f, 1f, 0f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        crosshair.SetActive(true);
        Player.maxHealth = health;
        Player.currentHealth = health;
        EnemySpawner.waves = 1;
        EnemySpawner.enemySpawnCounter = 0;
        EnemySpawner.enemyLiveCounter = 0;
        gameOver = false;
    }
}
