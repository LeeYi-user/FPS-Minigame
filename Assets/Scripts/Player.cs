﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public float currentHealth;

	public AudioSource source;
	public AudioClip clip;

	public Slider slider;
	public Gradient gradient;
	public Image fill;

	// Start is called before the first frame update
	void Start()
    {
		currentHealth = PlaySceneManager.health;

		SetMaxHealth(PlaySceneManager.health);
    }

    // Update is called once per frame
    void Update()
    {

	}

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;

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
		slider.value = health;
		fill.color = gradient.Evaluate(1f);
	}

	void SetHealth(float health)
	{
		slider.value = health;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
