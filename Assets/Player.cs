using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float maxHealth = 100f;
	public float currentHealth;

	public HealthBar healthBar;

	public GameOver gameOver;

    // Start is called before the first frame update
    void Start()
    {
		currentHealth = maxHealth;

		healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);

		if (currentHealth <= 0f)
        {
			gameOver.Setup("YOU LOSE");
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}
