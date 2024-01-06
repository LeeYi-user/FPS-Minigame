using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	[SerializeField] private PlaySceneManager playSceneManager;

	public static float maxHealth;
	public static float currentHealth;

	public AudioSource source;
	public AudioClip clip;

	public Slider slider;
	public Gradient gradient;
	public Image fill;

	// Start is called before the first frame update
	void Start()
    {
		maxHealth = PlaySceneManager.health;
		currentHealth = PlaySceneManager.health;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		if (transform.position.y < -10 && !PlaySceneManager.gameOver)
        {
			TakeDamage(currentHealth);
        }

		SetMaxHealth(maxHealth);
		SetHealth(currentHealth);
	}

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;

		if (currentHealth <= 0)
        {
			playSceneManager.Upgrade();
        }

		SetHealth(currentHealth);
		source.PlayOneShot(clip);
	}

	public void TakeHealth(float health)
    {
		currentHealth += health;

		if (currentHealth > PlaySceneManager.health)
        {
			currentHealth = PlaySceneManager.health;
        }

		SetHealth(currentHealth);
	}

	void SetMaxHealth(float health)
	{
		slider.maxValue = health;
	}

	void SetHealth(float health)
	{
		slider.value = health;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
