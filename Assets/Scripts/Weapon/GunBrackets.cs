using UnityEngine;

public class GunBrackets : MonoBehaviour
{
    public float damage = 10;
    public float range = 100;
    public float fireRate = 15;
    public float fireDelay = 0;
    public float impactForce = 60;

    public Transform barrelTip;
    public GameObject crossHair;

    public ParticleSystem muzzleEffect;
    public GameObject impactEffect;
    

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

        RaycastHit hit;
        if(Physics.Raycast(barrelTip.transform.position, barrelTip.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            TargetDamage target = hit.transform.GetComponent<TargetDamage>();
            if(target != null )
            {
                target.TakeDamage(damage);
            }
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.normal * impactForce);
            }
            GameObject impactGO = Instantiate(impactEffect, -hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
