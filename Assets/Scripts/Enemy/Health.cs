using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;
    Ragdoll ragdoll;
    SkinnedMeshRenderer skinnedMeshRenderer;

    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;

    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        currentHealth = maxHealth;

        // Get all Rigidbody components attached to this GameObject and its children
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            // Add a HitBox component to each Rigidbody's GameObject
            HitBox hitBox = rigidBody.gameObject.AddComponent<HitBox>();
            // Set the health reference in the HitBox to this Health script
            hitBox.health = this;
        }
    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
        if (currentHealth <= 0.0f)
        {
            Die();
        }

        blinkTimer = blinkDuration;

    }

    private void Die()
    {
        ragdoll.ActivateRagdoll();
    }

    private void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = lerp * blinkIntensity;
        skinnedMeshRenderer.material.color = Color.white * intensity;

    }
}
