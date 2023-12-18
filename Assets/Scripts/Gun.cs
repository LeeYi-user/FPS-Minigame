using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float throwRate = 1f;
    public float impactForce = 200;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public AudioSource source;
    public AudioClip clip;

    public bool gameOver;

    float nextTimeToFire = 0f;
    float nextTimeToThrow = 0f;

    public GameObject shurikenPrefab;
    public Transform shurikenHolder;

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

        if (Input.GetButtonDown("Fire2") && !gameOver && Time.time >= nextTimeToThrow)
        {
            nextTimeToThrow = Time.time + 1f / throwRate;
            Throw();
        }
    }

    void Throw()
    {
        GameObject shuriken = Instantiate(shurikenPrefab, shurikenHolder.position, Quaternion.identity);

        shuriken.GetComponent<Shuriken>().moveDirection = fpsCam.transform.forward;
        shuriken.transform.LookAt(shuriken.transform.position + fpsCam.transform.forward);
        shuriken.transform.Rotate(90, 0, 0);
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
            Landmine landmine = hit.transform.GetComponent<Landmine>();

            if (enemy)
            {
                enemy.TakeDamage(damage);
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
            else if (missile)
            {
                missile.TakeDamage(damage);
            }
            else if (boss)
            {
                boss.TakeDamage(damage);
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
            else if (landmine)
            {
                ScoreBoard.score += 5;
                Destroy(hit.transform.gameObject);
            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("whatIsGround"))
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
        }
    }
}
