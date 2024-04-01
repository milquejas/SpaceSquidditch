using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float impactForce = 0f;

    public void SetImpactForce(float force)
    {
        impactForce = force;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (impactForce > 0)
        {
            // Käsittele törmäyksen aiheuttama voima täällä
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = collision.contacts[0].point - transform.position;
                rb.AddForce(direction.normalized * impactForce, ForceMode.Force);
            }
        }

        // Tuhoa ammus törmäyksen jälkeen
        Destroy(gameObject);
    }
}
