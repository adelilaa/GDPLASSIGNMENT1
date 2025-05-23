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
    public float maxRotation = 45f;
    [HideInInspector]
    public bool hasLaunched = false;

    [SerializeField]
    private TrajectoryLine trajectoryLine;

    //place to spawn the balls
    public Transform muzzle;
    public GameObject BeachBall;

    //constraints for the shooter
    private float rotMin = 0f;
    private float rotMax = 0f;
    private float pitchMin = 0f;
    private float pitchMax = 0f;

    private void Awake()
    {
        //setting max and min rotation for the shooter
        rotMin = transform.rotation.eulerAngles.y - maxRotation;
        rotMax = transform.rotation.eulerAngles.y + maxRotation;
        pitchMin = transform.rotation.eulerAngles.x - maxRotation;
        pitchMax = transform.rotation.eulerAngles.x + maxRotation;
        Debug.Log(pitchMin + " " + pitchMax);

    }

    void Start()
    {
        currentForce = 1f;
        //rb = GetComponent<Rigidbody>();
        //trajectoryPredictor = GetComponent<TrajectoryPredictor>();
    }

    void Update()
    {
       //setting accurate trajectory line
        trajectoryLine.ShowTrajectoryLine(muzzle.position, muzzle.forward * currentForce);
        //rb.isKinematic = !hasLaunched;
        //if (hasLaunched) return;

        HandleAiming();
        HandleCharging();
        //Predict();

        if (HUD.instance != null)
        {
            //updates the HUD on the shooter position
            HUD.instance.setElevation(transform.localEulerAngles.x);
            HUD.instance.setHorizontal(transform.localEulerAngles.y);
            HUD.instance.setPower(currentForce / maxForce);
        }
    }

    void HandleAiming()
    {
        float horizontal = Input.GetAxis("Horizontal"); //A/D
        float vertical = Input.GetAxis("Vertical");     //W/S

        transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.right, -vertical * rotationSpeed * Time.deltaTime, Space.Self);

        Vector3 euler = transform.rotation.eulerAngles;


        if (transform.rotation.eulerAngles.y < rotMin) { transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotMin, 0f); }
        if (transform.rotation.eulerAngles.y > rotMax) { transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotMax, 0f); }

        
        float pitch = NormalizeAngle(euler.x);
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
        euler.x = pitch;

        transform.rotation = Quaternion.Euler(euler.x, transform.rotation.eulerAngles.y, 0f);
    }

    private float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        return angle;
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
            if (GameManager.instance.canShoot())
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
        GameManager.instance.UseShot();
        //rb.isKinematic = false;
        //hasLaunched = true;
        //isCharging = false;
        //rb.AddForce(transform.forward * currentForce, ForceMode.Impulse);
        //trajectoryPredictor.SetTrajectoryVisible(false); // Hide line after launch
    }


}