using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class Shockwave : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPreFab;
    private Rigidbody rb;
    private Collider _collider;

    private const float defaultForceMultiplier = 200f;

   

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>(); 
    }

    public void Release(Transform aimTransform, float extraForceMultiplier)
    {
        rb.AddForce(aimTransform.forward* defaultForceMultiplier * extraForceMultiplier);
        rb.useGravity = true;
        transform.SetParent(null);
        _collider.enabled = true;
    }
    public void OnCollisionEnter(Collision collision)
    {
        var explotionPosition = transform.position;
        Instantiate(explosionPreFab, explotionPosition, Quaternion.identity);
        Destroy(gameObject);
    }
}
