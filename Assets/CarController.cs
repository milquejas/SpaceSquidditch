using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody carRB;
    [SerializeField] Transform[] rayPoints;
    [SerializeField] LayerMask drivable;

    [Header("Suspension Settings")]
    [SerializeField] float springStiffness;
    [SerializeField] float damperStiffness;
    [SerializeField] float restLength;
    [SerializeField] float springTravel;
    [SerializeField] float wheelRadius;

    // Start is called before the first frame update
    void Start()
    {
        carRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Suspension();
    }
    private void Suspension()
    {
        foreach (Transform rayPoint in rayPoints) 
        {
            RaycastHit hit;
            float maxLength = restLength + springTravel;

            if(Physics.Raycast(rayPoint.position, -rayPoint.up, out hit, maxLength + wheelRadius, drivable))
            {
                float currentSpringLength = hit.distance - wheelRadius;
                float springCompression = (restLength - currentSpringLength) / springTravel;

                float springVelocity = Vector3.Dot(carRB.GetPointVelocity(rayPoint.position), rayPoint.up);
                float dampForce = damperStiffness * springVelocity;

                float springForce = springStiffness * springCompression;

                float netForce = springForce - dampForce;

                carRB.AddForceAtPosition(netForce * rayPoint.up, rayPoint.position);

                Debug.DrawLine(rayPoint.position, hit.point, Color.red);
            }
            else
            {
                Debug.DrawLine(rayPoint.position, rayPoint.position + (wheelRadius + maxLength) * -rayPoint.up, Color.green);
            }
        }
    }
}
