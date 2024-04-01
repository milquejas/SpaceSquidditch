using UnityEngine;

public class Shoot : MonoBehaviour
{
    public WeaponSO weaponData;
    [SerializeField] Transform shootingPoint;
    private float fireDelay;

    void Start()
    {
        if (weaponData.currentClip <= weaponData.maxClipSize && 
            weaponData.currentAmmo <= weaponData.maxAmmoSize)
        {
            weaponData.currentClip = weaponData.maxClipSize;
            weaponData.currentAmmo = weaponData.maxAmmoSize;
        }
    }

    void Update()
    {
        if (weaponData.currentClip <= weaponData.maxClipSize && Input.GetKeyDown(KeyCode.R))
        {
            Reload();
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= fireDelay)
        {
            fireDelay = Time.time + 1f / weaponData.fireRate;
            if (weaponData.currentClip > 0)
            {
                ShootB();
            }

        }
    }

    private void Reload()
    {
        int reloadAmount = weaponData.maxClipSize - weaponData.currentClip;
        reloadAmount = Mathf.Min(weaponData.maxClipSize - weaponData.currentClip, weaponData.currentAmmo);
        weaponData.currentClip += reloadAmount;
        weaponData.currentAmmo -= reloadAmount;
        //weaponData.currentClip = weaponData.maxClipSize;
    }

    void ShootB()
    {
        weaponData.currentClip--;

        if (weaponData.currentClip > -1)
        {
            GameObject bullet = Instantiate(weaponData.bullet, shootingPoint.position, shootingPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(shootingPoint.forward * weaponData.bulletSpeed, ForceMode.Force);
            Destroy(bullet, 1.5f);
        }
    }
    public void AddAmmo(int ammoAmount)
    {
        weaponData.currentAmmo += ammoAmount;
        if (weaponData.currentAmmo > weaponData.maxAmmoSize)
        {
            weaponData.currentAmmo = weaponData.maxAmmoSize;
        }
    }

}
