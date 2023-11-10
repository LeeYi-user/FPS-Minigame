using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 200;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public AudioSource source;
    public AudioClip clip;

    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !gameOver)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        source.PlayOneShot(clip);

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            Missile missile = hit.transform.GetComponent<Missile>();
            Boss boss = hit.transform.GetComponent<Boss>();

            if (enemy)
            {
                enemy.TakeDamage(damage);
            }
            else if (missile)
            {
                missile.TakeDamage(damage);
            }
            else if (boss)
            {
                boss.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
