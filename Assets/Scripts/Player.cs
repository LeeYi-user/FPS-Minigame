using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public float maxHealth = 100f;
	public float currentHealth;

	public GameOver GameOver;

	public AudioSource source;
	public AudioClip clip;

	public Slider slider;
	public Gradient gradient;
	public Image fill;

	// Start is called before the first frame update
	void Start()
    {
		currentHealth = maxHealth;

		SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.P))
        {
			maxHealth = 10000;
			currentHealth = maxHealth;

			SetMaxHealth(maxHealth);
        }

		if (transform.position.y < -5f && currentHealth > 0f)
		{
			TakeDamage(currentHealth);
		}
	}

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;

		SetHealth(currentHealth);
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

		SetHealth(currentHealth);
	}

	void SetMaxHealth(float health)
	{
		slider.maxValue = health;
		slider.value = health;
		fill.color = gradient.Evaluate(1f);
	}

	void SetHealth(float health)
	{
		slider.value = health;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
