using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector]
    public float dieForce; 
    public float currentHealth;
    Ragdoll ragdoll;
    SkinnedMeshRenderer skinnedMeshRenderer;
    UIHealthBar healthBar;

    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;

    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        healthBar = GetComponentInChildren<UIHealthBar>();
        currentHealth = maxHealth;

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            HitBox hitBox = rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.health = this;
        }
    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        if (currentHealth <= 0.0f)
        {
            Die(direction); // Updated to pass direction
        }

        blinkTimer = blinkDuration;
    }

    private void Die(Vector3 direction) // Updated Die method to accept Vector3 direction
    {
        ragdoll.ActivateRagdoll();
        direction.y = 1; 
        //ragdoll.ApplyForce(direction * dieForce); 
        healthBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1.0f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }
}
