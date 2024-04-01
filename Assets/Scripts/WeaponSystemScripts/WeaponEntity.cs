using UnityEngine;

public class WeaponEntity : MonoBehaviour
{
    public WeaponSO weaponData;
    public GameObject weapon;
    public GameObject bullet;
    public GameObject barrelTip;
    public ParticleSystem muzzleEffect;
    public TrailRenderer bulletTracer;
    public GameObject crossHair;
    
    public float range;
    public float spread;

    public float impactForce;
    public float damage;
    public float fireRate;
    public float fireDelay;

    public float reloadTime;
    public int clipSize;
    //void Start()
    //{
    //    barrelTip = weaponData.barrelTip;
    //    crossHair = weaponData.crossHair;
    //    weapon = weaponData.weapon;
    //    bullet = weaponData.bullet;
    //    muzzleEffect = weaponData.muzzleEffect;
    //    bulletTracer = weaponData.bulletTracer;

    //    range = weaponData.range;
    //    spread = weaponData.spread;
    //    damage = weaponData.damage;
    //    impactForce = weaponData.impactForce;
    //    fireRate = weaponData.fireRate;
    //    fireDelay = weaponData.fireDelay;
    //    reloadTime = weaponData.reloadTime;
    //    clipSize = weaponData.clipSize;
    //}

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= fireDelay)
        {
            Debug.Log("isFiring");
            fireDelay = Time.time + 1f / fireRate;
            Shoot();
        }

    }

    private void Shoot()
    {
        muzzleEffect.Play();
        Debug.Log("isShooting");
        RaycastHit hit;
        if (Physics.Raycast(barrelTip.transform.position, barrelTip.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            TargetDamage target = hit.transform.GetComponent<TargetDamage>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.normal * impactForce);
            }
            GameObject impactGO = Instantiate(bullet, -hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
            Debug.Log("ishit");
        }
    }
}
