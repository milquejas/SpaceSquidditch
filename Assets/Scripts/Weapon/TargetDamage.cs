using UnityEngine;

public class TargetDamage : MonoBehaviour
{
    public float healt;

    public void TakeDamage(float amount)
    {
        healt -= amount;
        if (healt < 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
