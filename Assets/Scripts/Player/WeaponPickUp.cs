using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public RaycastWeaponUpdate weaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.GetComponent<ActiveWeapon>();
        if (activeWeapon)
        {
            RaycastWeaponUpdate newWeapon = Instantiate(weaponPrefab);
            activeWeapon.Equip(newWeapon);
            Destroy(gameObject);
        }

        AiWeapons aiWeapons = other.gameObject.GetComponent<AiWeapons>();
        if (aiWeapons)
        {
            RaycastWeaponUpdate newWeapon = Instantiate(weaponPrefab);
            aiWeapons.Equip(newWeapon);
            Destroy(gameObject);
        }
    }
}
