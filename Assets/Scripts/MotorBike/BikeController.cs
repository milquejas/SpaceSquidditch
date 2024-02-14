using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MonoBehaviour
{
    RaycastHit hit;

    float moveInput, steerInput, rayLength, currentVelocityOffset;

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


    // Start is called before the first frame update
    void Start()
    {
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
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");

        transform.position = SphereRB.transform.position;

        velocity = BikeBody.transform.InverseTransformDirection(BikeBody.velocity);
        currentVelocityOffset = velocity.x / maxSpeed;
    }

    private void FixedUpdate()
    {
        Movement();

        // Visuals
        SkidMarks();
        FrontTyre.transform.Rotate(-Vector3.forward, Time.deltaTime * tyreRotSpeed * moveInput);
        BackTyre.transform.Rotate(-Vector3.forward, Time.deltaTime * tyreRotSpeed * moveInput);


        //SFX
        EngineSound();

    }
    void Movement()
    {
        if (Grounded())
        {
            if (!Input.GetKey(KeyCode.Space))
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
        SphereRB.velocity = Vector3.Lerp(SphereRB.velocity, moveInput * maxSpeed * -transform.right, Time.fixedDeltaTime * acceleration);
    }

    void Rotation()
    {
        transform.Rotate(0, steerInput * moveInput * turningCurve.Evaluate(Mathf.Abs(currentVelocityOffset))
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
        if (Input.GetKey(KeyCode.Space))
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
        if (Grounded() && Mathf.Abs(velocity.z) > minSkidVelocity || Input.GetKey(KeyCode.Space))
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
}
