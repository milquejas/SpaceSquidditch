using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void Shoot(WeaponSO weaponData);
    void Reload(WeaponSO weaponData);
    void Swap(WeaponSO weaponData);
}
