using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class RaycastWeaponUpdate : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer bulletTracer;
        public int bounce;
    }

    public ActiveWeapon.WeaponSlot weaponSlot;
    public string weaponName;
    public int maxBounce;
    public bool debug = false;
    public float bulletSpeed;
    public float bulletDrop;
    public float maxLifeTime;
    List<Bullet> bullets = new List<Bullet>();


    public bool isFiring = false;
    public int fireRate;
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;

    public Transform raycastOrigin;
    public Transform raycastDestination;

    Ray ray;
    RaycastHit hitInfo;
    float accumulatedTime;
    

    Vector3 GetPosition(Bullet bullet)
    {
        // Quadratic equation to calculate bullet Drop
        //  p + v * t + 0.5 * g * t * t
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time) + 
            (0.5f * bullet.time * bullet.time * gravity);
    }

    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.bulletTracer = Instantiate(tracerEffect, position, Quaternion.identity);
        bullet.bulletTracer.AddPosition(position);
        bullet.bounce = maxBounce;
        Color color = Random.ColorHSV(0.46f, 0.61f);
        float intensity = 20.0f;
        Color rgb = new Color(color.r * intensity, color.g * intensity, color.b * intensity, color.a * intensity);
        bullet.bulletTracer.material.SetColor("_EmissionColor", rgb);
        return bullet;
    }

    public void StartFiring()
    {
        isFiring = true;
        accumulatedTime = 0.0f;
        FireBullet();
    }

    public void UpdateWeapon(float deltaTime)
    {
        if (Input.GetButtonDown("Fire1") && !isFiring)
        {
            StartFiring();
        }
        if (isFiring)
        {
            UpdateFiring(deltaTime);
            UpdateBullets(deltaTime);

        }
        if (Input.GetButtonUp("Fire1") && isFiring)
        {
            StopFiring();
        }
    }

    public void UpdateFiring(float deltaTime)
    {
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate;
        while (accumulatedTime >= 0.0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }

    }

    public void UpdateBullets(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();
    }

    void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
    }

    void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.time > maxLifeTime);
    }

    void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        if (bullet.bulletTracer == null || bullet.bulletTracer == null)
        {
            // Lis‰‰ virheenk‰sittely t‰h‰n tarvittaessa
            return;
        }

        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;

        Color debugColor = Color.green;

        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            if (hitInfo.collider != null)
            {
                hitEffect.transform.position = hitInfo.point;
                hitEffect.transform.forward = hitInfo.normal;
                hitEffect.Emit(1);
            }

            //bullet.bulletTracer.transform.position = hitInfo.point;
            bullet.time = maxLifeTime;
            end = hitInfo.point;
            debugColor = Color.red;
        }

        // Bullet ricochet
        if (bullet.bounce > 0)
        {
            bullet.time = 0;
            bullet.initialPosition = hitInfo.point;
            bullet.initialVelocity = Vector3.Reflect(bullet.initialVelocity, hitInfo.normal);
            bullet.bounce--;
        }

        // Collision impulse
        var rb2d = hitInfo.collider.GetComponent<Rigidbody2D>();
        if (rb2d)
        {
            rb2d.AddForceAtPosition(ray.direction * 20, hitInfo.point, (ForceMode2D)ForceMode.Impulse);
        }


        bullet.bulletTracer.transform.position = end;

        if (debug)
        {
            UnityEngine.Debug.DrawLine(start, end, debugColor, 1.0f);
        }
    }
    private void FireBullet()
    {
        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }

        Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);

        if (bullet != null) // Lis‰‰ tarkistus t‰ss‰
        {
            bullets.Add(bullet);
        }
    }

    public void StopFiring()
    {
        isFiring = false;
    }


}
