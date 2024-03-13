using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    public Rigidbody rb;
    public GameObject camHolder;
    public float speed, sprintSpeed, sensitivity, maxForce, jumpForce;
    private Vector2 look;
    private Vector2 input;
    private float lookRotation;
    public bool isGrounded;
    private bool isSprinting;
    Animator animator;

    //// Jump variables
    public float gravityScale;
    public float jumpSpeed;
    public float fallingGravityScale;
    public float currentGravityScale;

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        animator.SetFloat("Input.X", input.x);
        animator.SetFloat("Input.Y", input.y);

    }
    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }
    //public void OnJump(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //        Jump();
    //}
    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValueAsButton();
    }
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        currentGravityScale = gravityScale;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode.Impulse);
        if (rb.velocity.y >= 0)
            currentGravityScale = gravityScale;
        else if (rb.velocity.y < 0)
            currentGravityScale = fallingGravityScale;
    }
    private void FixedUpdate()
    {
        Move();
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
    }
    private void LateUpdate()
    {
        //Look();
    }

    public void MountBike(BikeController bike)
    {
        // Disabloi pelaajan liikkuminen
        // Enabloi moottoripyörän ja disabloi pelaajan peliolio
        rb.isKinematic = true;
        bike.MountPlayer(this);
    }

    public void DismountBike(PlayerController playerController)
    {
        // Enabloi pelaajan liikkuminen
        playerController.enabled = true;

        // Disabloi moottoripyörä ja enabloi pelaajan peliolio
        rb.isKinematic = false;
        playerController.SetGrounded(true);

    }


    private void Move()
    {

        // Find target velocity
        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
        targetVelocity *= isSprinting ? sprintSpeed : speed;

        // Align direction
        targetVelocity = transform.TransformDirection(targetVelocity);

        // Calculate forces
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        // Limit force
        Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }
    private void Look()
    {
        // Turn
        transform.Rotate(look.x * sensitivity * Vector3.up);

        // Look
        lookRotation += (-look.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -45, 5);
        camHolder.transform.eulerAngles = new Vector3(lookRotation, camHolder.transform.eulerAngles.y, camHolder.transform.eulerAngles.z);
    }

    public void SetGrounded(bool state)
    {
        isGrounded = state;
    }


}
