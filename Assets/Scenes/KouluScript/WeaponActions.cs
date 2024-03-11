using UnityEngine;

public class WeaponActions : MonoBehaviour, IWeapon
{
    public void Shoot(WeaponSO weaponData)
    {
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
}
