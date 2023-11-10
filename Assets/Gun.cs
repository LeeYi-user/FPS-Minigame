using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 200;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public AudioSource source;
    public AudioClip clip;

    public bool gameOver;

    float nextTimeToFire = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponSwitch.selectedWeapon == 0)
        {
            if (Input.GetButtonDown("Fire1") && !gameOver && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && !gameOver && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
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
