using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField] WeaponSO weaponData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("hit");
            other.GetComponentInChildren<Shoot>().AddAmmo(weaponData.maxAmmoSize);
            Destroy(gameObject);
        }
    }
}
