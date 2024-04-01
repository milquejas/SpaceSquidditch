using UnityEngine;

public class CharacterAiming : MonoBehaviour
{
    public float aimDuration = 0.3f;
    public float turnSpeed = 15f;

    Camera mainCamera;
    RaycastWeaponUpdate weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponentInChildren<RaycastWeaponUpdate>();
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
        AimCam();
    }

    void AimCam()
    {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }
}
