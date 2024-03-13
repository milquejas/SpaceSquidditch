using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class GunSystem : MonoBehaviour
{
    // Gun stats
    public int damage;
    public float fireRate;
    public float spread;
    public float range;
    public float reloadTime;
    public float maxTimeBetweenShooting;
    public int magazineSize;
    public int bulletsPerTap;
    int bulletsLeft;
    int bulletsShot;

    // Conditions;
    public bool allowButtonHold;
    bool isShooting;
    bool isReadyToShoot;
    bool isRealoading;

    // Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    // Graphics
    public ParticleSystem muzzleFlash, bulletHoleGraphics;
    public ParticleSystem hitEffect;
    //public CamShake camShake;
    //public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        isReadyToShoot = true;
    }
    private void Update()
    {
       HandleInput();

        // SetText
        text.SetText(bulletsLeft + "/" + magazineSize);
    }

    private void HandleInput()
    {
        if (allowButtonHold) 
            isShooting = Input.GetKey(KeyCode.Mouse0);
        else 
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);

        // Reload
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !isRealoading) Reload();

        // Shoot
        if (isReadyToShoot && isShooting && !isRealoading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }

    }

    void Shoot()
    {
        isReadyToShoot = false;
        // Spread 
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        // Raycast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            //if (rayHit.collider.CompareTag("Enemy"))
            //    rayHit.collider.GetComponent<ShootingAi>().TakeDamage(damage);
        }
        // ShakeCamera
        //camShake.Shake(camShakeDuration, camShakeMagnitude);

        // Graphics
        Instantiate(bulletHoleGraphics, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash,attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShot", fireRate);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", fireRate);
    }

    void ResetShot()
    {
        isReadyToShoot = true;
    }

    private void Reload()
    {
        isRealoading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        isRealoading = false;
    }
}
