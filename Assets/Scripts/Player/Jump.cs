using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    // Jump variables
    public Rigidbody rb;
    public float gravityScale;
    public float jumpSpeed;
    public float fallingGravityScale;
    private float currentGravityScale;
    // Start is called before the first frame update

    void Start() => currentGravityScale = gravityScale;

    private void FixedUpdate() => rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode.Impulse);
        if(rb.velocity.y >= 0)
            currentGravityScale = gravityScale;
        else if (rb.velocity.y < 0)
            currentGravityScale = gravityScale;
    }



    
}
