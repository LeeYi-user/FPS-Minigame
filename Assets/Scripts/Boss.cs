using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private float health;

    public GameObject projectile;
    public GameObject destroyEffect;
    public AudioSource source;
    public AudioClip clip;

    float regenTime;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        health = EnemySpawner.waves * 100f;
        regenTime = 0.5f;
        timer = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlaySceneManager.gameOver)
        {
            Destroy(gameObject);
            return;
        }

        transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0));
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 5, 0), 5 * Time.deltaTime);

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Instantiate(projectile, transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)), Quaternion.identity);
            timer = regenTime;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        source.PlayOneShot(clip);

        if (health <= 0)
        {
            EnemySpawner.enemyLiveCounter--;
            GameObject destroyGO = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            PlaySceneManager.money += 2000;
            Destroy(destroyGO, 2f);
            Destroy(gameObject);
        }
    }
}
