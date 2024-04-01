//using UnityEngine;

//public class PlayerArmory : MonoBehaviour
//{
//    // Aseen tilastot scriptable objecteina
//    public WeaponSO primaryWeapon;
//    public WeaponSO secondaryWeapon;

//    // Viittaus pelaajan aseeseen
//    public GunSystem playerGun;

//    void Start()
//    {
//        // Aseistetaan pelaaja oletusaseella
//        EquipPrimaryGun(primaryWeapon);
//    }
//    public void EquipWeapon()
//    {

//    }
//    public void EquipPrimaryGun(WeaponSO gunStats)
//    {
//        // Asetetaan pelaajan valitun aseen tiedot
//        primaryWeapon = gunStats;

//        // P‰ivitet‰‰n pelaajan aseen tiedot GunSystem-skriptiss‰
//        playerGun.damage = primaryWeapon.damage;
//        playerGun.fireRate = primaryWeapon.fireRate;
//        playerGun.spread = primaryWeapon.spread;
//        playerGun.range = primaryWeapon.range;
//        playerGun.reloadTime = primaryWeapon.reloadTime;
//        playerGun.magazineSize = primaryWeapon.clipSize;
//        playerGun.bulletsPerTap = primaryWeapon.bulletsPerTap;

//        // P‰ivitet‰‰n pelaajan aseen grafiikat jne. tarvittaessa
//        // Esimerkiksi: playerGun.muzzleFlash = selectedGunStats.muzzleFlash;
//    }
//    public void EquipSecondaryGun(WeaponSO gunStats)
//    {
//        // Asetetaan pelaajan valitun aseen tiedot
//        secondaryWeapon = gunStats;

//        // P‰ivitet‰‰n pelaajan aseen tiedot GunSystem-skriptiss‰
//        playerGun.damage = secondaryWeapon.damage;
//        playerGun.fireRate = secondaryWeapon.fireRate;
//        playerGun.spread = secondaryWeapon.spread;
//        playerGun.range = secondaryWeapon.range;
//        playerGun.reloadTime = secondaryWeapon.reloadTime;
//        playerGun.magazineSize = secondaryWeapon.clipSize;
//        playerGun.bulletsPerTap = secondaryWeapon.bulletsPerTap;

//        // P‰ivitet‰‰n pelaajan aseen grafiikat jne. tarvittaessa
//        // Esimerkiksi: playerGun.muzzleFlash = selectedGunStats.muzzleFlash;
//    }
//    public void SwitchWeapon()
//    {
//        // Tarkista, kumpi ase on t‰ll‰ hetkell‰ k‰ytˆss‰
//        if (playerGun == primaryWeapon)
//        {
//            // Vaihda toissijaiseen aseeseen
//            EquipWeapon();
//        }
//        else
//        {
//            // Vaihda ensisijaiseen aseeseen
//            EquipWeapon();
//        }
//    }
//}
