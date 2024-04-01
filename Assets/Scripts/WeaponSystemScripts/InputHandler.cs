//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class InputHandler : MonoBehaviour
//{
//    private WeaponActions weaponActions;
//    public WeaponSO weaponData;
//    private void Start()
//    {
//        weaponActions = GetComponent<WeaponActions>();
//    }
//    void Update() 
//    {
//        if (Input.GetButtonDown("Fire1"))
//        {
//            // Kutsu Shoot-metodia ja välitä sille WeaponData-olio
//            weaponActions.Shoot(weaponData);
//        }

//        // Tarkista lataamisen input
//        if (Input.GetKeyDown(KeyCode.R))
//        {
//            // Kutsu Reload-metodia ja välitä sille WeaponData-olio
//            weaponActions.Reload(weaponData);
//        }

//        // Tarkista aseen vaihdon input
//        if (Input.GetKeyDown(KeyCode.Alpha1))
//        {
//            // Kutsu Swap-metodia ja välitä sille WeaponData-olio
//            weaponActions.Swap(weaponData);
//        }
//    }
//}
