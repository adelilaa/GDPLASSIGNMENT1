using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;



public class ProjectileThrow : MonoBehaviour
{
    public float chargeSpeed = 10f;
    public float maxForce = 30f;
    public float rotationSpeed = 50f;
    public int shotCount;
    private float currentForce = 0f;
    private bool isCharging = false;
    [HideInInspector]
    public bool hasLaunched = false;

    [SerializeField]
    private TrajectoryLine trajectoryLine;

    //Place to spawn beachball
    public Transform muzzle;
    public GameObject BeachBall;

    void Start()
    {
        currentForce = 1f;
        //rb = GetComponent<Rigidbody>();
        //trajectoryPredictor = GetComponent<TrajectoryPredictor>();
    }

    void Update()
    {
        trajectoryLine.ShowTrajectoryLine(muzzle.position, muzzle.forward * currentForce);
        //rb.isKinematic = !hasLaunched;
        //if (hasLaunched) return;

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
            if (shotCount > 0)
            {
                Throw();
            }
            
        }
    }

    void Predict()
    {
       // ProjectileProperties props = new ProjectileProperties
       // {
       //     initialPosition = transform.position,
       //     direction = transform.forward,
       //     initialSpeed = currentForce,
       //     mass = rb.mass,
       //     drag = rb.drag
       // };

       ////trajectoryPredictor.PredictTrajectory(props);
    }

    public void AddShot(int shotaddition)
    {
        shotCount += shotaddition;
    }

    //
    void Throw()
    {
        GameObject go = Instantiate(BeachBall, muzzle.position, Quaternion.identity);
        Rigidbody beachBallRB = go.GetComponent<Rigidbody>();
        beachBallRB.AddForce(muzzle.forward * currentForce, ForceMode.Impulse);
        shotCount -= 1;
        //rb.isKinematic = false;
        //hasLaunched = true;
        //isCharging = false;
        //rb.AddForce(transform.forward * currentForce, ForceMode.Impulse);
        //trajectoryPredictor.SetTrajectoryVisible(false); // Hide line after launch
    }
}