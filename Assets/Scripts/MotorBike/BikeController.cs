using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BikeController : MonoBehaviour
{
    RaycastHit hit;
    public PlayerController playerController;

    float rayLength, currentVelocityOffset;

    [HideInInspector] public Vector3 velocity;

    public Rigidbody SphereRB, BikeBody;
    public GameObject Handle;
    public GameObject FrontTyre;
    public GameObject BackTyre;
    public TrailRenderer skidMarks;
    public ParticleSystem smoke;
    public AudioSource engineSound;
    public AudioSource skidSound;

    public AnimationCurve turningCurve;

    public float maxSpeed, acceleration, steerStrenght, tiltAngle, gravity;
    public float bikeZTiltIncrement = 0.09f, xTiltAngle;
    public float handleRotVal = 30f, handleRotSpeed = 0.15f, tyreRotSpeed = 10000f;
    public float skidWidth = 0.062f, minSkidVelocity = 10f;
    public float normalDrag = 2f, driftDrag = 5f;
    [Range(1, 10)]
    public float brakingFactor;
    public LayerMask derivableSurface;

    [Range(0, 1)] public float minPitch;
    [Range(1, 5)] public float maxPitch;

    // Inputs
    private InputAction accelerate;
    private InputAction steer;
    private InputAction brake;

    float accelerateInput;
    float steerInput;
    float brakeInput;

    private bool playerOnBike = false;

    private void Awake()
    {

        accelerate = new InputAction("accelerate", InputActionType.Value);
        steer = new InputAction("steer", InputActionType.Value);
        brake = new InputAction("brake", InputActionType.Value);

        brake.performed += OnBrake;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerOnBike)
        {
            enabled = false;
        }

        SphereRB.transform.parent = null;
        BikeBody.transform.parent = null;

        rayLength = SphereRB.GetComponent<SphereCollider>().radius + 0.2f;

        //visuals
        skidMarks.startWidth = skidWidth;
        skidMarks.emitting = false;
        Smoke();

        //sfx
        skidSound.mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerOnBike)
        {
            // Jos pelaaja ei ole moottoripyörän kyydissä, disabloi BikeController
            enabled = false;
        }
        transform.position = SphereRB.transform.position;
        velocity = BikeBody.transform.InverseTransformDirection(BikeBody.velocity);
        currentVelocityOffset = velocity.x / maxSpeed;
    }

    private void FixedUpdate()
    {
        Movement();

        // Visuals
        SkidMarks();
        FrontTyre.transform.Rotate(-Vector3.forward, Time.deltaTime * tyreRotSpeed * accelerateInput);
        BackTyre.transform.Rotate(-Vector3.forward, Time.deltaTime * tyreRotSpeed * accelerateInput);


        //SFX
        EngineSound();

    }
    public bool PlayerOnBike()
    {
        return playerOnBike;
    }
    public void MountPlayer(PlayerController player)
    {
        // Enabloi pelaajan moottoripyörän kyytiin
        // Disabloi pelaajan peliolio ja asettaa pelaajan moottoripyörän lapsiobjektiksi
        //player.gameObject.SetActive(false);
        player.transform.parent = transform;
        playerOnBike = true;
        enabled = true;

    }

    public void DismountPlayer(PlayerController player)
    {
        // Disabloi pelaajan moottoripyörän kyydistä
        // Enabloi pelaajan peliolio ja irroittaa pelaajan moottoripyörästä
        player.gameObject.SetActive(true);
        player.transform.parent = null;

        playerOnBike = false;
        player.rb.isKinematic = false;
    }

    void Movement()
    {
        if (Grounded())
        {
            if (brakeInput < 0.5f)
            {
                Acceleration();
            }
            Rotation();
            Brake();
        }
        else
        {
            Gravity();
        }
        BikeTilt();
    }

    void Acceleration()
    {
        SphereRB.velocity = Vector3.Lerp(SphereRB.velocity, accelerateInput * maxSpeed * -transform.right, Time.fixedDeltaTime * acceleration);
    }

    void Rotation()
    {
        transform.Rotate(0, steerInput * accelerateInput * turningCurve.Evaluate(Mathf.Abs(currentVelocityOffset))
            * steerStrenght * Time.fixedDeltaTime, 0, Space.World);

        //visuals

        Handle.transform.localRotation = Quaternion.Slerp(Handle.transform.localRotation,
            Quaternion.Euler(Handle.transform.localRotation.eulerAngles.x,
            handleRotVal * steerInput, Handle.transform.localRotation.eulerAngles.z), handleRotSpeed);
    }

    void BikeTilt()
    {
        float zRot = (Quaternion.FromToRotation(BikeBody.transform.up, hit.normal) * BikeBody.transform.rotation).eulerAngles.z;
        float xRot = 0;

        if (currentVelocityOffset > 0)
        {
            xRot = xTiltAngle * steerInput * currentVelocityOffset;

        }

        Quaternion targetRot = Quaternion.Slerp(BikeBody.transform.rotation,
            Quaternion.Euler(zRot, transform.eulerAngles.y, xRot), bikeZTiltIncrement);

        Quaternion newRotation = Quaternion.Euler(targetRot.eulerAngles.z, transform.eulerAngles.y, targetRot.eulerAngles.x);

        BikeBody.MoveRotation(newRotation);
    }

    void Brake()
    {
        if (brakeInput > 0.5f)
        {
            SphereRB.velocity *= brakingFactor / 10;
            SphereRB.drag = driftDrag;
        }
        else
        {
            SphereRB.drag = normalDrag;
        }
    }

    bool Grounded()
    {
        float radius = rayLength - 0.02f;
        Vector3 origin = SphereRB.transform.position + radius * Vector3.up;

        if (Physics.SphereCast(origin, radius + 0.02f, -transform.up, out hit, rayLength, derivableSurface))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    void Gravity()
    {
        SphereRB.AddForce(gravity * Vector3.down, ForceMode.Acceleration);
    }

    void SkidMarks()
    {
        if (Grounded() && Mathf.Abs(velocity.z) > minSkidVelocity || brake.triggered)
        {
            skidMarks.emitting = true;

            skidSound.mute = false;
        }
        else
        {
            skidMarks.emitting = false;

            skidSound.mute = true;
        }
    }

    void EngineSound()
    {
        engineSound.pitch = Mathf.Lerp(minPitch, maxPitch, Mathf.Abs(currentVelocityOffset));
    }

    void Smoke()
    {
        if (skidMarks.emitting)
        {
            smoke.Play();
        }
        else
        {
            smoke.Stop();
        }
    }
    public void OnAccelerate(InputAction.CallbackContext context)
    {
        accelerateInput = context.ReadValue<float>();
    }

    public void OnSteer(InputAction.CallbackContext context)
    {
        steerInput = context.ReadValue<float>();
    }
    public void OnBrake(InputAction.CallbackContext context)
    {
        brakeInput = context.ReadValue<float>();
    }

    private void OnEnable()
    {
        
        accelerate.Enable();
        steer.Enable();
        brake.Enable();
    }

    private void OnDisable()
    {
        
        accelerate.Disable();
        steer.Disable();
        brake.Disable();
    }
}
