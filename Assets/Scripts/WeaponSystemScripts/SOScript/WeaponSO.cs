using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "SOWeapons/Create new weapon")]
public class WeaponSO : ScriptableObject
{
    public GameObject bullet;
    public float impactForce;
    public float bulletSpeed;
    public float range;
    public float spread;

    public float damage;
    public float fireRate;

    public int currentClip;
    public int maxClipSize;
    public int currentAmmo;
    public int maxAmmoSize;
}