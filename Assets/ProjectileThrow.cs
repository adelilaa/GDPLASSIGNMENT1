using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TrajectoryPredictor))]
public class ProjectileThrow : MonoBehaviour
{
    public float chargeSpeed = 10f;
    public float maxForce = 30f;
    public float rotationSpeed = 50f;

    private float currentForce = 0f;
    private bool isCharging = false;
    [HideInInspector]
    public bool hasLaunched = false;

    private Rigidbody rb;
    private TrajectoryPredictor trajectoryPredictor;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();
    }

    void Update()
    {
        rb.isKinematic = !hasLaunched;
        if (hasLaunched) return;

        HandleAiming();
        HandleCharging();
        //Predict();
    }

    void HandleAiming()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A/D
        float vertical = Input.GetAxis("Vertical");     // W/S

        transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.right, -vertical * rotationSpeed * Time.deltaTime, Space.Self);

        //GameManager.Instance.UpdateElevation(transform.rotation.y);
    }

    void HandleCharging()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            currentForce = 0f;
        }

        if (Input.GetKey(KeyCode.Space) && isCharging)
        {
            currentForce += chargeSpeed * Time.deltaTime;
            print(currentForce);
            currentForce = Mathf.Clamp(currentForce, 0f, maxForce);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isCharging)
        {
            Throw();
        }
    }

    void Predict()
    {
        ProjectileProperties props = new ProjectileProperties
        {
            initialPosition = transform.position,
            direction = transform.forward,
            initialSpeed = currentForce,
            mass = rb.mass,
            drag = rb.drag
        };

        trajectoryPredictor.PredictTrajectory(props);
    }

    void Throw()
    {
        rb.isKinematic = false;
        hasLaunched = true;
        isCharging = false;
        rb.AddForce(transform.forward * currentForce, ForceMode.Impulse);
        trajectoryPredictor.SetTrajectoryVisible(false); // Hide line after launch
    }
}