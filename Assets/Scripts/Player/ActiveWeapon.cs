using System;
using System.Collections;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1
    }
    
    public Transform crossHairTarget;
    public Animator rigController;
    public Transform[] weaponSlots;

    public Transform weaponParent;
    RaycastWeaponUpdate weapon;

    RaycastWeaponUpdate[] equipped_weapons = new RaycastWeaponUpdate[2];
    int activeWeaponIndex;

    bool isHolstered= false;
    private int activateWeaponIndex;

    // T‰m‰ alla oleva vain esimerkin vuoksi pelaajan hand rig control
    // Alla olevaa variaabelia k‰ytet‰‰n Equip methodin sis‰ll‰
    public UnityEngine.Animations.Rigging.Rig handIK;


    // Start is called before the first frame update
    void Start()
    {
        RaycastWeaponUpdate existingWeapon = GetComponentInChildren<RaycastWeaponUpdate>();
        if (existingWeapon)
        {
            Equip(existingWeapon);
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (weapon)
        {
            weapon.UpdateWeaponAction(Time.deltaTime);
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    bool isHolstered = rigController.GetBool("holster_weapon");
                    rigController.SetBool("holster_weapon", !isHolstered);
                }
            }
        }


        //var weapon = GetWeapon(activeWeaponIndex);
        //if (weapon && !isHolstered)
        //{
        //    if (Input.GetKeyDown(KeyCode.X))
        //    {
        //        ToggleActiveWeapon();
        //        Debug.Log("holster");
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    SetActiveWeapon(WeaponSlot.Primary);
        //    if (Input.GetKeyDown(KeyCode.Alpha2))
        //    {
        //        SetActiveWeapon(WeaponSlot.Secondary);
        //    }
        //}

    }
    RaycastWeaponUpdate GetWeapon(int index)
    {
        if (index < 0 || index >= equipped_weapons.Length)
        { return null; }
        return equipped_weapons[index];
    }
    public void Equip(RaycastWeaponUpdate newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var weapon = GetWeapon(weaponSlotIndex);
        if (weapon)
        {
            Destroy(weapon.gameObject);
        }
        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        weapon.transform.parent = weaponSlots[weaponSlotIndex];
        //weapon.recoil.playerCamera = playerCamera;
        weapon.transform.parent = weaponParent;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        rigController.Play("equip_" + weapon.weaponName);
    }

    private void SetActiveWeapon(int weaponSlot)
    {
        throw new NotImplementedException();
    }

    public void ToggleActiveWeapon()
    {
        bool isHolstered = rigController.GetBool("holster_weapon");
        if (isHolstered)
        {
            StartCoroutine(ActivateWeapon(activeWeaponIndex));
        }
        else
        {
            StartCoroutine(HolsterWeapon(activeWeaponIndex));
        }
    }

    void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int holsterIndex = activeWeaponIndex;
        int activateIndex = (int)weaponSlot;

        if (holsterIndex == activateIndex)
        {
            holsterIndex = -1;
        }
        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }
    IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    {
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(holsterIndex));
        activateWeaponIndex = activateIndex;
    }
    IEnumerator HolsterWeapon(int index)
    {
        isHolstered = true;
        var weapon = GetWeapon(index);
        if (weapon)
        {
            rigController.SetBool("holster_weapon", true);
            do
            {
                yield return new WaitForEndOfFrame();
            }
            while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
    }
    IEnumerator ActivateWeapon(int index)
    {
        var weapon = GetWeapon(index);
        if (weapon)
        {
            rigController.SetBool("holster_weapon", false);
            rigController.Play("equip_" + weapon.weaponName);
            do
            {
                yield return new WaitForEndOfFrame();
            }
            while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f);
            isHolstered = false;
        }
    }
}
