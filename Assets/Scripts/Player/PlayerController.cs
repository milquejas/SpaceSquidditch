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
    private Vector2 move;
    private float lookRotation;
    public bool isGrounded;
    private bool isSprinting;
    Animator animator;

    // Jump variables
    public float gravityScale;
    public float jumpSpeed;
    public float fallingGravityScale;
    public float currentGravityScale;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        animator.SetFloat("Move.X", move.x);
        animator.SetFloat("Move.Y", move.y);

    }
    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            Jump();
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValueAsButton();
    }
    public void Start()
    {
        currentGravityScale = gravityScale;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        Move();
        
    }
    private void LateUpdate()
    {
        Look();
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
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
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

    // Tämä method asettaa voiman suoraan kappaleen nopeusvectoriin
    //void Jump()
    //{
    //    Vector3 jumpForces = rb.velocity;

    //    if (grounded)
    //    {
    //        jumpForces.y = jumpForce;
    //    }

    //    rb.velocity = jumpForces;
    //}

    // Tämä method käyttää fysiikkamoottoria hyppyvoiman laskemiseen
    //void Jump()
    //{
    //    Vector3 jumpForces = Vector3.zero;

    //    if (isGrounded)
    //    {
    //        jumpForces = Vector3.up * jumpForce;
    //    }

    //    rb.AddForce(jumpForces, ForceMode.VelocityChange);
    //}
    void Jump()
    {
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
    }


    //public static float CalculateJumpForce(float playerWeight, float playerStrength)
    //{
    //    // Lasketaan hyppyvoima pelaajan painon ja vahvuuden perusteella
    //    float jumpForce = playerWeight * playerStrength;
    //    return jumpForce;
    //}

    //public static float CalculateJumpHeight(float jumpForce, float gravity)
    //{
    //    // Lasketaan hyppykorkeus käyttäen hyppyvoimaa ja painovoimaa
    //    float jumpHeight = (jumpForce * jumpForce) / (2 * gravity);
    //    return jumpHeight;
    //}



    public void SetGrounded(bool state)
    {
        isGrounded = state;
    }

}
