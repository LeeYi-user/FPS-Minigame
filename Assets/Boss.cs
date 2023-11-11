using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject projectile;

    public float health;
    public GameObject destroyEffect;
    public AudioSource source;
    public AudioClip clip;

    float regenTime;
    float timer;

    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        regenTime = 1f;
        timer = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            return;
        }

        transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0));
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(5, 3, 0), 5 * Time.deltaTime);

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Instantiate(projectile, transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)), Quaternion.identity);
            timer = regenTime;

            if (regenTime > 0.2f)
            {
                regenTime -= 0.04f;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        source.PlayOneShot(clip);

        if (health <= 0)
        {
            GameObject destroyGO = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            ScoreBoard.score += 800;
            Destroy(destroyGO, 2f);
            Destroy(gameObject);
        }
    }
}
