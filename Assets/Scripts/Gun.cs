using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float range;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject[] impactEffect;

    [SerializeField] private float reloadTime;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Animator animator;

    [SerializeField] private Transform BulletSpawnPoint;
    [SerializeField] private TrailRenderer BulletTrail;
    [SerializeField] private float BulletSpeed;

    private bool isReloading;
    private int currentAmmo;
    private float nextTimeToFire;

    private void Start()
    {
        isReloading = false;
        currentAmmo = PlaySceneManager.ammo;
        nextTimeToFire = 0f;
    }

    private void FixedUpdate()
    {
        if (PlaySceneManager.gameOver)
        {
            isReloading = false;
            currentAmmo = PlaySceneManager.ammo;
            nextTimeToFire = 0f;
            return;
        }

        if ((Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1)) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            return;
        }

        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && Cursor.lockState == CursorLockMode.Locked)
        {
            nextTimeToFire = Time.time + 1f / PlaySceneManager.fireRate;
            Shoot();
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("isReloading", true);

        yield return new WaitForSeconds(reloadTime);

        isReloading = false;
        animator.SetBool("isReloading", false);

        currentAmmo = PlaySceneManager.ammo;
    }

    private void Shoot()
    {
        currentAmmo--;

        muzzleFlash.Play();
        audioSource.PlayOneShot(audioClip);

        RaycastHit hit;
        int MadeImpact = 0;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            MadeImpact = 1;

            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                MadeImpact = 2;
                hit.transform.gameObject.GetComponent<Enemy>().TakeDamage(PlaySceneManager.damage);
            }
            else if (hit.transform.gameObject.CompareTag("Boss"))
            {
                MadeImpact = 2;
                hit.transform.gameObject.GetComponent<Boss>().TakeDamage(PlaySceneManager.damage);
            }
            else if (hit.transform.gameObject.CompareTag("Missile"))
            {
                Destroy(hit.transform.gameObject);
            }
        }

        CreateBulletTrail_ServerRpc(BulletSpawnPoint.position, true, hit.point, hit.normal, MadeImpact, fpsCam.transform.forward);
    }

    private void CreateBulletTrail_ServerRpc(Vector3 position, bool IsReal, Vector3 HitPoint, Vector3 HitNormal, int MadeImpact, Vector3 forward)
    {
        TrailRenderer trail = Instantiate(BulletTrail, position, Quaternion.identity);

        if (MadeImpact > 0)
        {
            StartCoroutine(SpawnTrail(trail, HitPoint, HitNormal, MadeImpact, IsReal));
        }
        else
        {
            StartCoroutine(SpawnTrail(trail, position + forward * range, Vector3.zero, MadeImpact, IsReal));
        }
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, int MadeImpact, bool IsReal)
    {
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));
            remainingDistance -= BulletSpeed * Time.deltaTime;
            yield return null;
        }

        Trail.transform.position = HitPoint;

        if ((MadeImpact > 0) && IsReal)
        {
            GameObject impactGO = Instantiate(impactEffect[MadeImpact - 1], HitPoint, Quaternion.LookRotation(HitNormal));
            Destroy(impactGO, 2f);
        }

        Destroy(Trail.gameObject, Trail.time);
    }
}
