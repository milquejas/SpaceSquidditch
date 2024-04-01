using System;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    int totalWeapons = 1;
    public int currentWeaponIndex;
    public GameObject weaponHolder;
    public GameObject[] weapons;
    public GameObject currentWeapon;
    public static event Action<WeaponSO> OnWeaponSwitched;

    // Start is called before the first frame update
    void Start()
    {


        totalWeapons = weaponHolder.transform.childCount;
        weapons = new GameObject[totalWeapons];

        for (int i = 0; i < totalWeapons; i++)
        {
            weapons[i] = weaponHolder.transform.GetChild(i).gameObject;
            weapons[i].SetActive(false);
        }

        weapons[0].SetActive(true);
        currentWeapon = weapons[0];
        currentWeaponIndex = 0;

        SendWeaponSwitchEvent(currentWeapon.GetComponent<Shoot>().weaponData);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            SwitchWeapon(currentWeaponIndex + 1);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            SwitchWeapon(currentWeaponIndex - 1);
        }
    }
    void SwitchWeapon(int newIndex)
    {
        // If the new index (newIndex) is less than zero OR
        //the new index is greater than OR equal to the total number of weapons (totalWeapons),
        //then perform the following actions.

        if (newIndex < 0 || newIndex >= totalWeapons)
            return;

        weapons[currentWeaponIndex].SetActive(false);
        currentWeaponIndex = newIndex;
        weapons[currentWeaponIndex].SetActive(true);
        currentWeapon = weapons[currentWeaponIndex];

        // Lähetä tapahtuma asevaihdosta ja lähetä uuden aseen tiedot.
        SendWeaponSwitchEvent(currentWeapon.GetComponent<Shoot>().weaponData);
    }
    void SendWeaponSwitchEvent(WeaponSO weaponData)
    {
        OnWeaponSwitched?.Invoke(weaponData);
    }
}

