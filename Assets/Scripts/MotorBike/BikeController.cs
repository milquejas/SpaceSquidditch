 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MonoBehaviour
{
    float moveInput, steerInput;

    public Rigidbody SphereRB, BikeBody;
    public float maxSpeed;
    public float acceleration, steerStrenght;
    // Start is called before the first frame update
    void Start()
    {
        SphereRB.transform.parent = null;
        BikeBody.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");

        transform.position = SphereRB.transform.position;
        BikeBody.MoveRotation(transform.rotation);
    }

    private void FixedUpdate()
    {
        Movement();
        Rotation();
    }

    void Movement()
    {
        SphereRB.velocity = Vector3.Lerp(SphereRB.velocity, moveInput * maxSpeed * transform.forward, Time.fixedDeltaTime * acceleration);
    }

    void Rotation()
    {
        transform.Rotate(0, steerInput * moveInput * steerStrenght * Time.fixedDeltaTime, 0, Space.World);
    }
}
