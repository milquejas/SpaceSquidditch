using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadWeapon : MonoBehaviour
{
    public Animator rigController;
    public WeaponAnimationEvents animationEvents;
    public ActiveWeapon activeWeapon;
    public Transform leftHand;

    GameObject magazineHand;
    // Start is called before the first frame update
    void Start()
    {
        //animationEvents.WeaponAnimationEvents.AddListener(OnAnimationEvents);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            rigController.SetTrigger("reload_weapon");
        }
    }
    void onAnimationEvent(string eventName)
    {
        Debug.Log(eventName);
        switch (eventName)
        {
            case "detach_magazine":
                break;
            case "drop_magazine":
                break;
            case "refill_magazine":
                break;
            case "attach_magazine":
                break;
        }
    }
    void DetachMagazine()
    {
        //RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        //magazineHand = Instantiate(weapon.magazine, leftHand, true);
        //weapon.magazine.SetActive(false);

    }
    void DropMagazine()
    {
        GameObject droppedMagazine = Instantiate(magazineHand, magazineHand.transform.position, magazineHand.transform.rotation);
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.AddComponent<BoxCollider>();
        magazineHand.SetActive(false);
    }
    void RefillMagazine()
    {
        magazineHand.SetActive(true);
    }
    void AttachMagazine()
    {
        //RaycastWeaponUpdate weapon = activeWeapon.GetActiveWeapon();
        //weapon.magazine.SetActive(true);
        Destroy(magazineHand);
    }
}
