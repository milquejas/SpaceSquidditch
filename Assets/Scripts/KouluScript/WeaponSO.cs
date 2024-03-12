using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "So/Create new weapon")]
public class WeaponSO : ScriptableObject
{
    public GameObject gameObject;
    public float range;    
    public float accuracy;
    public float spread;
    public float fireRate;  
    public float reloadTime;
    public int maxClipSize;
    public int clipSize;
    public int damage;
}
