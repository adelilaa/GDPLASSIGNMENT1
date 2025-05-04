using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TrajectoryPredictor))]
public class ProjectileThrow : MonoBehaviour
{
    [SerializeField] private Rigidbody objectToThrow;
    [SerializeField, Range(0.0f, 50.0f)] private float force = 20f;
    [SerializeField] private Transform startPosition;
    public InputAction fire; // This can be manually bound in Inspector

    private TrajectoryPredictor predictor;

    private void OnEnable()
    {
        predictor = GetComponent<TrajectoryPredictor>();

        if (startPosition == null)
            startPosition = transform;

        fire.Enable();
        fire.performed += ThrowObject;
    }

    private void OnDisable()
    {
        fire.performed -= ThrowObject;
        fire.Disable();
    }

    private void Update()
    {
        Predict();
    }

    void Predict()
    {
        var data = new ProjectileProperties
        {
            direction = startPosition.forward,
            initialPosition = startPosition.position,
            initialSpeed = force,
            mass = objectToThrow.mass,
            drag = objectToThrow.drag
        };

        predictor.PredictTrajectory(data);
    }

    void ThrowObject(InputAction.CallbackContext ctx)
    {
        Rigidbody thrownObject = Instantiate(objectToThrow, startPosition.position, Quaternion.identity);
        thrownObject.AddForce(startPosition.forward * force, ForceMode.Impulse);
    }
}