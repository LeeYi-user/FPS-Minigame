using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float maxHealth = 100f;
	public float currentHealth;

	public HealthBar healthBar;

	public GameOver GameOver;

	public AudioSource source;
	public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
		currentHealth = maxHealth;

		healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.P))
        {
			maxHealth = 10000;
			currentHealth = maxHealth;

			healthBar.SetMaxHealth(maxHealth);
        }

		if (transform.position.y < -5f && currentHealth > 0f)
		{
			TakeDamage(currentHealth);
		}
	}

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);
		source.PlayOneShot(clip);

		if (currentHealth <= 0f)
        {
			GameOver.Setup("YOU LOSE");
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	public void TakeHealth(float health)
    {
		currentHealth += health;

		if (currentHealth > maxHealth)
        {
			currentHealth = maxHealth;
        }

		healthBar.SetHealth(currentHealth);
	}
}
