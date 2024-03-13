using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponBehaviour : MonoBehaviour
{

    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform projectileInitTransform;
    [SerializeField]
    private Transform aimTransform;

    //Tässä haetaan shockwave scripti ja annetaan sille muuttuja
    private Shockwave _currentProjectile;

    private Vector3 maxScale;
    private bool charging;
    private const float chargeSpeed = 0.05f;
    private float releaseExtraForce = 1f;
    private const float forceMultiplier = 0.5f;


    public void HandleFireAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            
            Debug.Log("Click");
            var projectile = Instantiate(projectilePrefab, projectileInitTransform);
            _currentProjectile = projectile.GetComponent<Shockwave>();

            maxScale = _currentProjectile.transform.localScale;
            _currentProjectile.transform.localScale /= 2f;
            releaseExtraForce = 1f;

            charging = true;
        }

        if (context.canceled)
        {
            Debug.Log("Release");
            _currentProjectile.Release(aimTransform, releaseExtraForce);

            charging = false;
        }
    }
    private void Update()
    {
        if (charging)
        {
            _currentProjectile.transform.localScale += Vector3.one * Time.deltaTime * chargeSpeed;

            releaseExtraForce += Time.deltaTime * forceMultiplier;
            Debug.Log("release extraforce");

            if (_currentProjectile.transform.localScale.x >= maxScale.x)
            {
                charging = false;
                Debug.Log("Shockwave released");
            }
        }
    }
}
