using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour, IWeapon
{
    //private PlayerActions playerActions; // Ota yhteys PlayerActions-skriptiin
    public WeaponSO weaponData;

    void Update() 
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Kutsu Shoot-metodia ja välitä sille WeaponData-olio
            Shoot(weaponData);
        }

        // Tarkista lataamisen input
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Kutsu Reload-metodia ja välitä sille WeaponData-olio
            Reload(weaponData);
        }

        // Tarkista aseen vaihdon input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Kutsu Swap-metodia ja välitä sille WeaponData-olio
            Swap(weaponData);
        }
    }
    public void Reload(WeaponSO weaponData)
    {
        Debug.Log("Shooting with " + weaponData.name);
    }

    public void Shoot(WeaponSO weaponData)
    {
        Debug.Log("Reloading " + weaponData.name);
    }

    public void Swap(WeaponSO weaponData)
    {
        Debug.Log("Swapping to " + weaponData.name);    }

}
