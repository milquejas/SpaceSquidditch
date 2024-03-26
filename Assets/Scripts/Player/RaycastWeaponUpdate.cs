using System.Collections.Generic;
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
    WeaponRecoil recoil;

    // WeaponSpecs
    //public MeshSocket.SocketId holsterSocket;
    public bool isFiring = false;
    public string weaponName;
    public int fireRate = 25;
    public float damage = 10.0f;
    public GameObject magazine;
    //public WeaponRecoil recoil;

    // BulletSpecs
    List<Bullet> bullets = new List<Bullet>();
    public float bulletSpeed = 1000.0f;
    public float bulletDrop = 300.0f;
    public float maxLifeTime = 3.0f;
    public int maxBounce = 1;
    public int ammoCount;
    public int clipSize;
    
    // Effects
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;

    //Transform
    public Transform raycastOrigin;
    public Transform raycastDestination;
    Vector3 target;
    public LayerMask layerMask;

    Ray ray;
    RaycastHit hitInfo;
    float accumulatedTime = 0.0f;
    private void Awake()
    {
        //recoil = GetComponent<WeaponRecoil>();
    }
    private void LateUpdate()      
    {
        UpdateWeaponAction(Time.deltaTime);
    }
    Vector3 GetPosition(Bullet bullet)
    {
        // Quadratic equation to calculate bullet Drop
        //  p + v * t + 0.5 * g * t * t
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time) +
            (0.5f * gravity * bullet.time * bullet.time);
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

    public void UpdateWeaponAction(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartFiring();
        }
        if (isFiring)
        {
            Debug.Log("Update");
            UpdateFiring(deltaTime);
        }

        // Need to keep track of cooldown even when not firing to prevent click spam.
        accumulatedTime += deltaTime;

        UpdateBullets(deltaTime);
        if (Input.GetMouseButtonUp(0))
        {
            StopFiring();
        }
    }

    public void StopFiring()
    {
        Debug.Log("Stop");
        isFiring = false;
        accumulatedTime = 0.0f;
    }

    public void StartFiring()
    {
        accumulatedTime = 0.0f;
        
        isFiring = true;
        //recoil.Reset();
        FireBullet();
        Debug.Log("Start");
    }

    public void UpdateFiring(float deltaTime)
    {    
        float fireInterval = 1.0f / fireRate;
        while (accumulatedTime >= 0.0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }
    private void FireBullet()
    {
        
        if (ammoCount <= 0)
        {
            return;
        }

        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }

        Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);

        bullets.Add(bullet);
        Debug.Log("bullet added");
        //recoil.GenerateRecoil(weaponName);
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
        bullets.RemoveAll(bullet => bullet.time >= maxLifeTime);
    }


    void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        // Tarkistetaan, ett� luodin j�lki ja luoti itsess��n eiv�t ole null-arvoisia
        if (bullet.bulletTracer == null || bullet.bulletTracer == null)
        {
            Debug.Log("Bullet on null");
            return;
        }

        // Lasketaan luodin suunta ja et�isyys aloitus- ja loppupisteiden v�lill�
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;

        // Suoritetaan raycast ja tallennetaan osutun kohteen tiedot hitInfo-muuttujaan
        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            // Jos luoti osuu johonkin, asetetaan iskuefekti ja siirret��n luodin j�lki osumakohdan paikkaan
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            // P�ivitet��n luodin j�ljen paikka ja asetetaan luodin elinaika maksimiin
            bullet.bulletTracer.transform.position = hitInfo.point;
            bullet.time = maxLifeTime;
            // P�ivitet��n loppupiste osumakohdan mukaan, jotta luoti voi kimpoilla osumisen j�lkeen
            end = hitInfo.point;
        }
        else
        {
            // Jos luoti ei osu mihink��n, siirret��n luodin j�lki loppukohtaan
            bullet.bulletTracer.transform.position = end;
        }

        // K�sitell��n luodin kimpoaminen
        if (bullet.bounce > 0)
        {
            bullet.time = 0;
            bullet.initialPosition = hitInfo.point;
            bullet.initialVelocity = Vector3.Reflect(bullet.initialVelocity, hitInfo.normal);
            bullet.bounce--;
        }

        // K�sitell��n t�rm�yksen aiheuttama impulssi
        var rb = hitInfo.collider ? hitInfo.collider.GetComponent<Rigidbody>() : null;
        if (rb)
        {
            rb.AddForceAtPosition(ray.direction * 20, hitInfo.point, ForceMode.Impulse);
        }

        // K�sitell��n t�rm�ykseen liittyv� toiminnallisuus
        var hitBox = hitInfo.collider ? hitInfo.collider.GetComponent<HitBox>() : null;
        if (hitBox)
        {
            hitBox.OnRaycastHit(this, ray.direction);
        }

        // P�ivitet��n luodin j�ljen paikka sen mukaan, osuiko luoti mihink��n
        if (bullet.bulletTracer != null)
        {
            if (hitInfo.collider != null)
            {
                bullet.bulletTracer.transform.position = hitInfo.point;
            }
            else
            {
                bullet.bulletTracer.transform.position = end;
            }
        }
    }






}
