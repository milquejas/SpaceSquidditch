using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultGunStats", menuName = "SO/Create new weapon")]
public class WeaponSO : ScriptableObject
{
    public GameObject Weapon;

    public float range;    
    public float accuracy;
    public float spread;
    public float fireRate;
    public float reloadTime;

    public int bulletsPerTap;
    public int clipSize;
    public int maxClipSize;
    public int damage;
}
