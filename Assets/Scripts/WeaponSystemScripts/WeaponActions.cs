using System;
using UnityEngine;

public class WeaponActions : MonoBehaviour, IWeaponAction
{
    WeaponSO weaponData;
    [SerializeField] GameObject weaponHolder;
    public void Awake()
    {
        weaponData = weaponHolder.GetComponent<WeaponSO>();
    }
    public void Start()
    {
        EquipGun(weaponData);
    }
    public void Aim()
    {

    }
    public void Shoot(WeaponSO weaponData)
    {
        // Onko valmis ampumaan?
        // Onko Lataus kesken ?
        // Onko t‰ht‰ys ylh‰‰ll‰? 
        // Onko ammuksia ? 
        // 
        Debug.Log("Shooting with " + weaponData.name);

        // V‰henn‰ ammusm‰‰r‰‰
        weaponData.clipSize--;

        // Aiheuta vahinkoa (t‰ss‰ vain esimerkki, tee tarvittavat muutokset)
        Debug.Log("Dealing " + weaponData.damage + " damage to target");

        // Tarkista, onko ammuksia j‰ljell‰
        if (weaponData.clipSize <= 0)
        {
            Debug.Log("Out of ammo!");
            // Voit lis‰t‰ tarvittavat toimenpiteet, kuten lataamisen automaattisesti, jos haluat
        }
    }

    public void Reload(WeaponSO weaponData)
    {
        Debug.Log("Reloading " + weaponData.name);

        // Palauta ammusm‰‰r‰ maksimiin
        weaponData.clipSize = weaponData.maxClipSize;

        // Aseta latausaika (tarvittaessa)
        // weaponData.reloadTime = ...;
    }

    public void Swap(WeaponSO weaponData)
    {
        Debug.Log("Swapping to " + weaponData.name);

        // Aseta vaihdettu ase aktiiviseksi (tarvittaessa)
    }

    public void EquipGun(WeaponSO weaponData)
    {
        throw new System.NotImplementedException();
    }
}
