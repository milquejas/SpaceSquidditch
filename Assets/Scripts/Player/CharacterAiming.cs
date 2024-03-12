using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15f;
    Camera mainCamera;

    public Rig aimLayer;
    public float aimDuration = 0.3f;

    RaycastWeaponUpdate weapon;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weapon = GetComponentInChildren<RaycastWeaponUpdate>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
        AimCam();
    }

    private void LateUpdate()
    {
        AimWeapon();
    }

    void AimCam()
    {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }

    void AimWeapon()
    {
        if (aimLayer)
        {
            if (Input.GetButton("Fire2"))
            {
                aimLayer.weight += Time.deltaTime / aimDuration;
            }
            else
            {
                aimLayer.weight -= Time.deltaTime / aimDuration;
            }
        }


        if (Input.GetButtonDown("Fire1")) 
        {
            weapon.StartFiring();
        }
        if (weapon.isFiring)
        {
            weapon.UpdateFiring(Time.deltaTime);
        }

        weapon.UpdateBullets(Time.deltaTime);

        if (Input.GetButtonUp("Fire1"))
        {
            weapon.StopFiring();
        }
    }
}
