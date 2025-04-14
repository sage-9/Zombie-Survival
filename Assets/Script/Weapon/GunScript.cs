using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{
    [Header("Gun Settings")]
    public float fireRate = 0.2f; // Time between shots
    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 1.5f;
    public bool isReloading = false;

    [Header("Shooting")]
    public float damage = 20f;
    public float range = 100f;
    public Transform firePoint;
    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;

    [Header("Recoil & Spread")]
    public float recoilAmount = 2f;
    public float bulletSpread = 0.1f;
    public Camera fpsCam;

    [Header("Crosshair")]
    public RectTransform crosshair;
    public float crosshairExpandAmount = 10f;

    private float nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (isReloading)
            return;

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    public void Shoot()
    {
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        currentAmmo--;

        if (muzzleFlash != null)
            muzzleFlash.Play();

        Vector3 direction = fpsCam.transform.forward;
        direction.x += Random.Range(-bulletSpread, bulletSpread);
        direction.y += Random.Range(-bulletSpread, bulletSpread);

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }

        ApplyRecoil();
        ExpandCrosshair();
    }

    void ApplyRecoil()
    {
        fpsCam.transform.localRotation *= Quaternion.Euler(-recoilAmount, 0, 0);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void ExpandCrosshair()
    {
        if (crosshair != null)
        {
            crosshair.sizeDelta = new Vector2(50 + crosshairExpandAmount, 50 + crosshairExpandAmount);
            StartCoroutine(ResetCrosshair());
        }
    }

    IEnumerator ResetCrosshair()
    {
        yield return new WaitForSeconds(0.1f);
        crosshair.sizeDelta = new Vector2(50, 50);
    }
}
