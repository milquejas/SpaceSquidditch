using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public RaycastWeaponUpdate weaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.GetComponent<ActiveWeapon>();
        if (activeWeapon)
        {
            RaycastWeaponUpdate newWeapon = Instantiate(weaponPrefab);
            activeWeapon.Equip(newWeapon);
        }
    }
}
