using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    //T‰‰ pit‰ˆ‰ aktovoida kun Mike on saanut Ampumis scriptin tehty‰
    public Health health;
    public void OnRaycastHit(RaycastWeaponUpdate weapon, Vector3 direction)
    {
        health.TakeDamage(weapon.damage, direction);
    }
}